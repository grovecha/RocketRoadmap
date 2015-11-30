//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using RocketRoadmap.DB;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RocketRoadmap.DB.Tests
//{
//    [TestClass()]
//    public class BusinessValueTest
//    {
//        [TestMethod()]
//        public void BusinessValueConstructorTest()
//        {
//            BusinessValue bv = new BusinessValue("test", "Test");

//            Assert.IsNotNull(bv);
//        }

//        [TestMethod()]
//        public void BusinessValueGetDescriptionTest()
//        {
//            BusinessValue bv = new BusinessValue("test", "Test");
//            bv.SetDescription("test");
//            Assert.IsTrue(bv.GetDescription() == "test");
//        }

//        [TestMethod()]
//        public void BusinessValueSetDescriptionTest()
//        {
//            BusinessValue bv = new BusinessValue("test", "Test");
//            bv.SetDescription("test1");
//            Assert.IsTrue(bv.GetDescription() == "test1");
//            bv.SetDescription("test");
//        }


//        [TestMethod()]
//        public void BusinessValueSetNameTest()
//        {
//            BusinessValue bv = new BusinessValue("test", "Test");
//            Assert.IsTrue(bv.SetName("test3"));
//            bv.SetName("test");
//        }

//        [TestMethod()]
//        public void ReOrderTest()
//        {
//            RoadMaps maps = new RoadMaps();
//            maps.CreateRoadMap("busboxtest", "test123", "test");
//            RoadMap newroadmap = new RoadMap("busboxtest");
//            StrategyPoint strat1 = new StrategyPoint("StratBox0", "first", "busboxtest");

//            newroadmap.AddStrategyPoint(strat1);

//            BusinessValue bis1 = new BusinessValue("StratBox0BusBox0", "busboxtest");
//            strat1.CreateBusinessValue("StratBox0BusBox0", "first", "busboxtest");

//            Project proj1 = new Project("StratBox0BusBox0ProjBox0", "first", "StratBox0BusBox0", "busboxtest");
//            Project proj2 = new Project("StratBox0BusBox0ProjBox1", "second", "StratBox0BusBox0", "busboxtest");
//            Project proj4 = new Project("StratBox0BusBox0ProjBox2", "fourth", "StratBox0BusBox0", "busboxtest");
//            bis1.CreateNewProject(proj1);
//            bis1.CreateNewProject(proj2);
//            bis1.CreateNewProject(proj4);

//            bis1.ReorderProject("StratBox0BusBox0ProjBox2", "third", true);
//            Project proj3 = new Project("StratBox0BusBox0ProjBox2", "third", "StratBox0BusBox0","busboxtest");
//            bis1.CreateNewProject(proj3);
//            bis1.ReloadProjects();
//            List<Project> list = bis1.GetProjects();
//            Assert.IsTrue(list.Last().GetName() == "StratBox0BusBox0ProjBox3");
//            maps.DeleteRoadMap("busboxtest");
//        }
//        [TestMethod()]
//        public void DeleteProjTest()
//        {
//            RoadMaps maps = new RoadMaps();
//            maps.CreateRoadMap("busboxtest", "test123", "test");
//            RoadMap newroadmap = new RoadMap("busboxtest");
//            StrategyPoint strat1 = new StrategyPoint("StratBox0", "first", "busboxtest");

//            newroadmap.AddStrategyPoint(strat1);
//            strat1.CreateBusinessValue("StratBox0BusBox0", "first", "busboxtest");
//            BusinessValue bis1 = new BusinessValue("StratBox0BusBox0", "busboxtest");

//            Project proj1 = new Project("StratBox0BusBox0ProjBox0", "first", "StratBox0BusBox0", "busboxtest");
//            Project proj2 = new Project("StratBox0BusBox0ProjBox1", "second", "StratBox0BusBox0", "busboxtest");
//            Project proj3 = new Project("StratBox0BusBox0ProjBox2", "third", "StratBox0BusBox0", "busboxtest");
//            bis1.CreateNewProject(proj1);
//            bis1.CreateNewProject(proj2);
//            bis1.CreateNewProject(proj3);

//            bis1.DeleteProject("StratBox0BusBox0ProjBox0");
//            bis1.ReloadProjects();
//            List<Project> projlist = bis1.GetProjects();
//            Assert.IsTrue(projlist.Last().GetName() == "StratBox0BusBox0ProjBox1");
//            maps.DeleteRoadMap("busboxtest");
//        }

//    }
//}
