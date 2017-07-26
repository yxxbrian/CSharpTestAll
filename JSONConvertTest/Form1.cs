using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;


namespace JSONConvertTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Person person1 = new Person("Bruce Lee", 37,SEX.male);
            Person person2 = new Person("Bruce Willis", 58, SEX.male);
            MessageBox.Show("result:"+person1.CompareTo(person2));
            List<Person> personList = new List<Person>() { person1, person2 };
            PersonComparer personComparer = new PersonComparer();
            personList.Sort();
            personList.Sort(personComparer);


            
            //string str = JsonConvert.SerializeObject(person);
            //Person newperson = JsonConvert.DeserializeObject<Person>(str);

            //Dog dog = new Dog();
            //(dog as IAnimal).Bark();

            //List<string> list = new List<string>() { "ss","bb"};
            //IEnumerator<string> enumerator =  list.GetEnumerator();
            //while (enumerator.MoveNext())
            //{
            //    MessageBox.Show(enumerator.Current);
            //}
            //string name1 = "Bruce Willis";
            //string name2 = "Bruce Willis";
            //int result = name1.CompareTo(name2);
            //MessageBox.Show("Result :"+result);
        }
    }

    class PersonComparer : IComparer<Person>
    {
        public int Compare(Person a,Person b) 
        {
            if (a.Age > b.Age)
                return 1;
            else if (a.Age == b.Age)
                return 0;
            else return -1;
        }

    }


    enum SEX
    {
        male = 1,
        female
    }
}
