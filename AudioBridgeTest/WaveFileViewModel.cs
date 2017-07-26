using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WaveLibrary;
using GongSolutions.Wpf.DragDrop;
using System.Collections;
using System.Reflection;
using System.Windows.Controls;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.Diagnostics;

namespace WaveEditor
{
    public class WaveFileViewModel : WorkspaceViewModel
    {
        public WaveProjectManager WPROM;
        public WaveEditWindowViewModel WaveEditVM = null;
        private List<WaveChannelViewModel> BackupWaveFileChannels;

        public WaveFileViewModel(WaveEditWindowViewModel wevm)
        {
            this.WaveEditVM = wevm;
            this.WaveFileName = String.Empty;
            this.WaveFileChannels = new ObservableCollection<WaveChannelViewModel>();

            this.WaveFileChannels.CollectionChanged += new NotifyCollectionChangedEventHandler(List_CollectionChanged);
        }

        public WaveFileViewModel(WaveFile wf, WaveEditWindowViewModel wevm, WaveFileAppendingData waveFileAppendingData = null)
        {
            this.WaveEditVM = wevm;
            this.WaveFileName = wf.FILENAME;
            string temp;
            ObservableCollection<WaveChannelViewModel> waveChannels = new ObservableCollection<WaveChannelViewModel>();
            if (waveFileAppendingData == null)
            {
                //默认的ChannelName
                for (ushort i = 0; i < wf.Head.NumChannels; i++)
                {
                    temp = "Channel" + i.ToString();
                    waveChannels.Add(new WaveChannelViewModel(temp, i, this));
                }
            }
            else
            {
                foreach (WaveChannelModel waveChM in waveFileAppendingData.Channels)
                {
                    waveChannels.Add(new WaveChannelViewModel(waveChM.Name, waveChM.Index, this));
                }
            }

            //ChannelName赋值时，会将HasBeenChanged设置为true。
            //但此时并不是用户在更改ChannelName，因此应该改回false
            HasBeenChanged = false;
            int k = 0;
            //默认的ChannelID
            foreach (WaveChannelViewModel cm in waveChannels)
            {
                cm.ChannelIndex = k++;
            }
            this.WaveFileChannels = waveChannels;
            this.WaveFileChannels.CollectionChanged += new NotifyCollectionChangedEventHandler(List_CollectionChanged);
            //WaveFileChangedStatusVisibility = Visibility.Collapsed;

            WPROM = new WaveProjectManager(wf);
        }

        /// <summary>
        /// 在WaveProjectManager::InitBlockSectionManager之后调用。
        /// </summary>
        public void SetChannelsManager()
        {
            if (WaveFileChannels != null && WPROM != null)
            {
                for (int i = 0; i < WaveFileChannels.Count; i++)
                {
                    WaveFileChannels[i].BSManager = WPROM.BSM[i];
                    WaveFileChannels[i].ManagerPC = WPROM.PointCacheManager[i];
                }
            }
        }

        /// <summary>
        /// 重新设置该WaveFileViewModel所在的WaveProjectManager中，BSM以及PointCacheManager列表的序列；保存更新后的状态
        /// </summary>
        /// <param name="WEVM"></param>
        /// <param name="waveOperationDefine"></param>
        /// <param name="updateWaveform">true:更新波形区域为该文件（默认更新）；false:不更新</param>
        public void ResetWaveProjectManager(WaveEditWindowViewModel WEVM, WaveOperationDefine waveOperationDefine, bool updateWaveform = true)
        {
            bool bChanged = false;
            //与前次状态进行比较，判断是否发生变更。
            if (WaveFileChannels.Count != BackupWaveFileChannels.Count)
            {
                bChanged = true;
                HasBeenChanged = true;
            }
            else
            {
                for (int i = 0; i < WaveFileChannels.Count; i++)
                {
                    if (WaveFileChannels[i] != BackupWaveFileChannels[i])
                    {
                        bChanged = true;
                        HasBeenChanged = true;
                        break;
                    }
                }
            }

            if (!bChanged)
            {
                return;
            }
            else
            {
                //为了更新BackupWaveFileChannels
                WaveFileChannels = WaveFileChannels;
            }
            //Todo:BSM以及PointCacheManager列表可能存在多线程问题。
            if (WPROM != null && WaveFileChannels != null && WaveFileChannels.Count >= 0)
            {
                if (WPROM.BSM != null)
                {
                    WPROM.BSM.Clear();
                    foreach (WaveChannelViewModel item in WaveFileChannels)
                    {
                        if (item != null)
                        {
                            WPROM.BSM.Add(item.BSManager);
                        }
                    }
                }
                if (WPROM.PointCacheManager != null)
                {
                    WPROM.PointCacheManager.Clear();
                    foreach (WaveChannelViewModel item in WaveFileChannels)
                    {
                        if (item != null)
                        {
                            WPROM.PointCacheManager.Add(item.ManagerPC);
                        }
                    }
                }

                WPROM.SET_OPTION.WAVE_TOTAL = WaveFileChannels.Count;
            }
            //保存改变后的状态
            if (WEVM != null)
            {
                Int32[] Channels = new Int32[WaveFileChannels.Count];
                for (int i = 0; i < WaveFileChannels.Count; i++)
                {
                    Channels[i] = i;
                }
                WEVM.SaveWorkSpace(this, 0, waveOperationDefine, Channels);

                //更新波形
                if (updateWaveform)
                {
                    WEVM.ReDrawWaveform(this);
                }
            }

        }

        private bool hasBeenChanged = false;
        public bool HasBeenChanged
        {
            get { return hasBeenChanged; }
            set
            {
                hasBeenChanged = value;
                OnPropertyChanged("HasBeenChanged");
                OnPropertyChanged("WaveFileChangedStatusVisibility");
            }
        }

        private Visibility waveFileChangedStatusVisibility = Visibility.Hidden;
        public Visibility WaveFileChangedStatusVisibility
        {
            get
            {
                if (HasBeenChanged)
                {
                    waveFileChangedStatusVisibility = Visibility.Visible;
                }
                else
                {
                    waveFileChangedStatusVisibility = Visibility.Hidden;
                }
                return waveFileChangedStatusVisibility;
            }
            set
            {
                waveFileChangedStatusVisibility = value;
                OnPropertyChanged("WaveFileChangedStatusVisibility");
            }
        }


        /// <summary>
        /// 0:有音频源文件 1:new出来的文件
        /// </summary>
        public int FileType = 0;

        public bool DoesWaveFileContainChannelData(int channelIndex, Int64 maxSample)
        {
            if (WPROM != null && WPROM.SET_OPTION != null
                && channelIndex >= 0
                && WPROM.SET_OPTION.WAVE_TOTAL > channelIndex
                && WPROM.GetTotalSampleNum() >= maxSample)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wavSrc"></param>
        /// <param name="wavDst"></param>
        /// <returns>0：可以合并； 非0：无法合并（1：参数异常；2：采样率不一致；3：采样位数不一致）</returns>
        public static int TryCombineFile(WaveFileViewModel wavSrc, WaveFileViewModel wavDst, out string appendInfo)
        {
            appendInfo = "";
            //用户new出来的空文件，SampleRate为0，可以任意进行合并
            if (wavDst.WPROM.SET_OPTION.SampleRate==0)
            {
                return 0;
            }

            if (wavSrc == null || wavDst == null
                || wavSrc.WPROM == null || wavDst.WPROM == null
                || wavSrc.WPROM.SET_OPTION == null || wavDst.WPROM.SET_OPTION == null)
            {
                appendInfo = "Some errors occurs when try to combine these two files!";
                return 1;
            }
            if (wavSrc.WPROM.SET_OPTION.SampleRate != wavDst.WPROM.SET_OPTION.SampleRate)
            {
                appendInfo = "Diffrent Sample Rate!";
                return 2;
            }
            //todo:判断采样位数
            //if (wavSrc.WPROM. != wavDst.WPROM.SET_OPTION.SampleRate)
            //{
            //    appendInfo = "Diffrent Sample Rate!";
            //    return 2;
            //}
            return 0;
        }

        private void List_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //WaveFileChangedStatusVisibility = Visibility.Visible;
            int k = 0;
            foreach (WaveChannelViewModel cm in WaveFileChannels)
            {
                cm.ChannelIndex = k++;
            }
        }

        //private Visibility waveFileChangedStatusVisibility = Visibility.Collapsed;
        //public Visibility WaveFileChangedStatusVisibility
        //{
        //    get
        //    {
        //        return waveFileChangedStatusVisibility;
        //    }
        //    set
        //    {
        //        if (waveFileChangedStatusVisibility != value)
        //        {
        //            waveFileChangedStatusVisibility = value;
        //            OnPropertyChanged("WaveFileChangedStatusVisibility");
        //        }
        //    }
        //}

        private string _waveFileName;
        public string WaveFileName
        {
            get
            {
                return _waveFileName;
            }
            set
            {
                if (_waveFileName != value)
                {
                    _waveFileName = value;
                    OnPropertyChanged("WaveFileName");
                    OnPropertyChanged("DisplayWaveFileName");
                }
            }
        }

        const int maxDisplayWaveFileNameLength = 30;
        /// <summary>
        /// 用于显示的文件名，左侧部分省略号显示
        /// </summary>
        public string DisplayWaveFileName
        {
            get
            {
                string displayWaveFileName = WaveFileName;
                if (WaveFileName.Length > maxDisplayWaveFileNameLength)
                {
                    displayWaveFileName = "..." + WaveFileName.Substring(WaveFileName.Length - maxDisplayWaveFileNameLength);

                }
                return displayWaveFileName;
            }
        }

        private ObservableCollection<WaveChannelViewModel> _waveFileChannels;
        public ObservableCollection<WaveChannelViewModel> WaveFileChannels
        {
            get
            {
                return _waveFileChannels;
            }
            set
            {
                //if (_waveFileChannels != value)
                {
                    _waveFileChannels = value;
                    if (_waveFileChannels != null)
                    {
                        BackupWaveFileChannels = new List<WaveChannelViewModel>(_waveFileChannels);
                    }
                    else
                    {
                        BackupWaveFileChannels = null;
                    }

                    OnPropertyChanged("WaveFileChannels");
                }
            }
        }

        private bool waveFileIsSelected;

        public bool WaveFileIsSelected
        {
            get { return waveFileIsSelected; }
            set
            {
                waveFileIsSelected = value;
                OnPropertyChanged("WaveFileIsSelected");
            }
        }

        #region RemoveFileCommand
        private ICommand removeFileCommand;
        public ICommand RemoveFileCommand
        {
            get
            {
                if (removeFileCommand == null)
                {
                    removeFileCommand = new RelayCommand(
                        p => OnRemoveFileCommand(),
                        p => CanRunRemoveFileCommand()
                        );
                }
                return removeFileCommand;
            }
        }
        public void OnRemoveFileCommand()
        {
            if (WaveEditVM != null && WaveEditVM.WaveFileViewModelList != null)
            {
                if (HasBeenChanged)
                {
                    MessageWindow mw = new MessageWindow(1, 1, "Do you want to remove it without saving?");
                    mw.Owner = App.Current.MainWindow;
                    Nullable<bool> result = mw.ShowDialog();
                    if (result == true)
                    {
                        WaveEditVM.WaveFileViewModelList.Remove(this);
                    }
                }
                else
                {
                    WaveEditVM.WaveFileViewModelList.Remove(this);
                }
                if (WaveEditVM.WaveFileViewModelList.Count > 0)
                {
                    WaveEditVM.SelectedWaveFileVM = WaveEditVM.WaveFileViewModelList[0];
                }
            }
        }
        public bool CanRunRemoveFileCommand()
        {
            if (WaveEditVM != null && WaveEditVM.RecordingMacro)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region SaveFileCommand
        private ICommand saveFileCommand;
        public ICommand SaveFileCommand
        {
            get
            {
                if (saveFileCommand == null)
                {
                    saveFileCommand = new RelayCommand(
                        p => OnSaveFileCommand(),
                        p => CanRunSaveFileCommand()
                        );
                }
                return saveFileCommand;
            }
        }
        private bool refreashWaveFileName = true;

        public bool RefreashWaveFileName
        {
            get { return refreashWaveFileName; }
            set
            {
                refreashWaveFileName = value;
                OnPropertyChanged("RefreashWaveFileName");
            }
        }
        
        public void OnSaveFileCommand()
        {
            if (this.WPROM == null)
            {
                MessageWindow messageWindow = new MessageWindow(0, 1, "Empty file!");
                messageWindow.Owner = App.Current.MainWindow;
                messageWindow.ShowDialog();
                return;
            }
            MainWindow mw = App.Current.MainWindow as MainWindow;
            BackgroundWorker worker = new BackgroundWorker();
            if (this.FileType == 1)
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.FileName = this.WaveFileName;
                dlg.Filter = "Wave file(*.wav)|*.wav|All files(*.*)|*.*";
                Nullable<bool> result = dlg.ShowDialog();
                if (result == true)
                {
                    this.WaveFileName = dlg.FileName;
                    this.RefreashWaveFileName = !RefreashWaveFileName;//通过这种方法刷新界面上显示的文件名
                    worker.DoWork += (o, ea) =>
                        {
                            WaveFileAppendingData waveFileAppendingData = new WaveFileAppendingData(this);
                            string appendingData = JsonConvert.SerializeObject(waveFileAppendingData);
                            this.WPROM.WriteToFile(dlg.FileName, appendingData);
                            this.WPROM.ResetStateStack();
                            HasBeenChanged = false;
                            WaveFileChannels = WaveFileChannels;
                            this.FileType = 0;
                        };
                    worker.RunWorkerCompleted += (o, ea) =>
                    {
                        System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            mw.busyIndicator.IsBusy = false;
                        }), null);
                    };
                    mw.busyIndicator.IsBusy = true;
                    worker.RunWorkerAsync();

                }

            }
            else
            {
                worker.DoWork += (o, ea) =>
                {
                    WaveFileAppendingData waveFileAppendingData = new WaveFileAppendingData(this);
                    string appendingData = JsonConvert.SerializeObject(waveFileAppendingData);
                    this.WPROM.WriteToFile(this.WaveFileName, appendingData);
                    this.WPROM.ResetStateStack();
                    HasBeenChanged = false;
                    WaveFileChannels = WaveFileChannels;
                };
                worker.RunWorkerCompleted += (o, ea) =>
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        mw.busyIndicator.IsBusy = false;
                    }), null);
                };
                mw.busyIndicator.IsBusy = true;
                worker.RunWorkerAsync();
            }



        }
        public bool CanRunSaveFileCommand()
        {
            return HasBeenChanged;
        }
        #endregion

        #region DeleteChannels
        private ICommand deleteChannelsCommand;

        public ICommand DeleteChannelsCommand
        {
            get
            {
                if (deleteChannelsCommand == null)
                {
                    deleteChannelsCommand = new RelayCommand(
                        p => OnDeleteChannelsCommand(),
                        p => CanRunDeleteChannelsCommand()
                        );
                }
                return deleteChannelsCommand;
            }
        }
        private void OnDeleteChannelsCommand()
        {
            ObservableCollection<WaveChannelViewModel> selectedChannel = new ObservableCollection<WaveChannelViewModel>();
            foreach (WaveChannelViewModel channelVM in this.WaveFileChannels)
            {
                if (channelVM.ChannelIsSelected)
                {
                    selectedChannel.Add(channelVM);
                }
            }
            foreach (WaveChannelViewModel channelVM in selectedChannel)
            {
                //记录到宏中
                if (WaveEditVM.RecordingMacro)
                {
                    DelChannel delChannel = new DelChannel();
                    delChannel.DstChannelIndex = WaveFileChannels.IndexOf(channelVM);
                    delChannel.DstFileIndex = WaveEditVM.WaveFileViewModelList.IndexOf(this);
                    WaveEditVM.EditMacro.EditOperations.Add(delChannel);
                }
                this.WaveFileChannels.Remove(channelVM);
            }
            HasBeenChanged = true;
            //保存改变后的状态并更新
            ResetWaveProjectManager(WaveEditVM, WaveOperationDefine.DETETECHANNEL, true);

        }

        private bool CanRunDeleteChannelsCommand()
        {
            //当前正在播放，不允许删除
            if (WaveEditVM != null && WaveEditVM.CurrentState.isState(WaveLibrary.TestStateValue.Playing))
            {
                return false;
            }

            ObservableCollection<WaveChannelViewModel> selectedChannel = new ObservableCollection<WaveChannelViewModel>();
            foreach (WaveChannelViewModel channelVM in this.WaveFileChannels)
            {
                if (channelVM.ChannelIsSelected)
                {
                    selectedChannel.Add(channelVM);
                }
            }
            if (selectedChannel.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        #endregion

    }
}
