using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RocketRoadmap.DB;

namespace RocketRoadmap
{
    public partial class datapulltest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string mapname = Request.QueryString["n"];
            RoadMap map = new RoadMap(mapname);
            List<StrategyPoint> splist = map.GetStrategyPoints();
            List<BusinessValue> bvlist = splist.First().GetBusinessValues();
            List<Project> projlist = bvlist.First().GetProjects();
            List<Issue> issuelist = projlist.First().GetIssues();
            List<Link> linklist = projlist.First().GetLinks();
            List<Project> dependencylist = projlist.First().GetDependencies();


            RoadmapName.Text = RoadmapName.Text + " " + mapname ;
            SP.Text = SP.Text + " " + splist.First().GetName();
            BV.Text = BV.Text + " " + bvlist.First().GetName();
            BVDescrip.Text = BVDescrip.Text + " " + bvlist.First().GetDescription();
            Proj.Text = Proj.Text + " " + projlist.First().GetName();
            ProjDescrip.Text = ProjDescrip.Text + " " + projlist.First().GetDescription();
            ProjStartDate.Text = ProjStartDate.Text + " " + projlist.First().GetStartDate().ToString();
            ProjEndDate.Text = ProjEndDate.Text + " " + projlist.First().GetEndDate().ToString();
            IssueDescrip.Text = IssueDescrip.Text + " " + issuelist.First().GetDescription();
            Link.Text = Link.Text + " " + linklist.First().GetLink();
            Dependency.Text = Dependency.Text + " " + dependencylist.First().GetName();

            

        }
    }
}