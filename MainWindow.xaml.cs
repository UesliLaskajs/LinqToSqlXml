using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
namespace LinqtoSql
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        LinqToSqlClassesDataContext dataContext;
          public MainWindow()
        {
            InitializeComponent();

            string DatabaseString = ConfigurationManager.ConnectionStrings["LinqtoSql.Properties.Settings.UesliDatabaseConnectionString1"].ConnectionString;//Connect DataBaseString
            dataContext =new LinqToSqlClassesDataContext(DatabaseString);//After A LinqToSql Class Created Create A dataContext To Insert Tables
            //InsertUniversities();
            //InsertStudents();
            //InsertLecture();
            //GetAllStudentsYale();
            GetAllLecturesFromYale();
        }

        public void InsertUniversities()
        {
            University yale = new University(); //Class Tabel University
            yale.Name = "Yale";//Add A Name
            dataContext.Universities.InsertOnSubmit(yale);//Insert On Submit

            University bejingTech=new University();
            bejingTech.Name = "BejingTech";
            dataContext.Universities.InsertOnSubmit(bejingTech);



            dataContext.SubmitChanges();//All Changes Made Submited
            MainGrid.ItemsSource= dataContext.Universities;//Ienumerable Form Of Display Universities 

        }

        public void InsertStudents()
        {
            University bejingTech = dataContext.Universities.First(un => un.Name.Equals("BejingTech"));
            University yale = dataContext.Universities.First(un => un.Name.Equals("Yale"));

            List<Student> students = new List<Student>();//Created A List Of Student Table Class
            students.Add(new Student { Name = "Uesli", Gender = "Male", University = bejingTech });//Added To Class New Student
            students.Add(new Student { Name = "Martin", Gender = "Male", University = yale });
            students.Add(new Student { Name = "Marvi" ,Gender="Male",University=bejingTech});



            dataContext.SubmitChanges();
            MainGrid.ItemsSource= dataContext.Students;
        }


        public void InsertLecture()
        {
            dataContext.Lectures.InsertOnSubmit(new Lecture { Name = "Math" });
            dataContext.Lectures.InsertOnSubmit(new Lecture { Name = "History" });

            dataContext.SubmitChanges();

            MainGrid.ItemsSource = dataContext.Lectures;
        }

        public void InsertStudentLectures()
        {
            Student Uesli = dataContext.Students.First(it => it.Equals("Uesli"));
            Student Enso = dataContext.Students.First(it => it.Equals("Martin"));
            Student Marvi = dataContext.Students.First(it => it.Equals("Marvi"));

            Lecture Math = dataContext.Lectures.First(it => it.Equals("Math"));
            Lecture History = dataContext.Lectures.First(it => it.Equals("History"));


            StudentLecture slUesli = new StudentLecture();
            slUesli.Student = Uesli;
            slUesli.Lecture = Math;
            dataContext.StudentLectures.InsertOnSubmit(slUesli);
            dataContext.StudentLectures.InsertOnSubmit(new StudentLecture { Student=Uesli,Lecture=Math });

            dataContext.SubmitChanges();
            MainGrid.ItemsSource= dataContext.StudentLectures;
        }

        public void getUniversityUesli()
        {
           Student Uesli= dataContext.Students.First(item => item.Equals("Uesli"));
            University UesliUni = Uesli.University;
          

            List<University> UesliUnis= new List<University>();
            UesliUnis.Add(UesliUni);

            MainGrid.ItemsSource= UesliUnis;
        }

        public void GetAllStudentsYale()
        {
            var studentYale = from students in dataContext.Students
                              where students.University.Name.Equals("Yale")
                              select students;

            MainGrid.ItemsSource= studentYale;
        }

        public void GetAllLecturesFromYale()
        {
            var UniLectures = from sl in dataContext.StudentLectures
                              join student in dataContext.Students on sl.StudentId equals student.Id
                              where student.University.Name == "Yale"
                              select sl.Lecture;

            MainGrid.ItemsSource= UniLectures;
        }

        public void UpdateStudent()
        {
            Student Marvi = dataContext.Students.FirstOrDefault(item => item.Equals("Marvi"));
            Marvi.Name = "Navai";
            dataContext.SubmitChanges();
            MainGrid.ItemsSource = dataContext.Students;
        }

        public void DeleteStudent()
        {
            Student Marvi = dataContext.Students.FirstOrDefault(item => item.Equals("Marvi"));
      
            dataContext.Students.DeleteOnSubmit(Marvi);
            dataContext.SubmitChanges();
            MainGrid.ItemsSource = dataContext.Students;
        }


    }


    
   
}
