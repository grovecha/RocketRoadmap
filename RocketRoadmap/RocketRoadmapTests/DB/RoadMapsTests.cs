﻿//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using RocketRoadmap.DB;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RocketRoadmap.DB.Tests
//{
//    [TestClass()]
//    public class RoadMapsTests
//    {
//        [TestMethod()]
//        public void GetAllMapsTest()
//        {
//            RoadMaps Maps = new RoadMaps();

//            List<RoadMap> test = Maps.GetAllMaps();

//            RoadMap one = new RoadMap("Test");

//        }

//        [TestMethod()]
//        public void GetUserMapsTest()
//        {
//            RoadMaps Maps = new RoadMaps();

//            List<RoadMap> test = Maps.GetUserMaps("123");

//            RoadMap one = new RoadMap("Test");

//        }

//        [TestMethod()]
//        public void Create_DeleteRoadMapsTest()
//        {
//            RoadMaps Maps = new RoadMaps();

//            Assert.IsTrue(Maps.CreateRoadMap("New", "NEW", "123"));

//            Assert.IsTrue(Maps.DeleteRoadMap("New"));
//        }

//    }
//}