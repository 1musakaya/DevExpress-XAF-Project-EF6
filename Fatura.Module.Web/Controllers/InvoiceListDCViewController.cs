using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.Validation;
using Fatura.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fatura.Module.Web.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class InvoiceListDCViewController : ViewController
    {
        // Use CodeRush to create Controllers and Actions with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/403133/
        public InvoiceListDCViewController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetObjectType = typeof(InvoiceListDC);
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            if (View is DetailView)
            {
                if (((DetailView)View).CurrentObject == null)
                {
                    ((DetailView)View).CurrentObject = ObjectSpace.CreateObject<InvoiceListDC>();
                }
                 ((DetailView)View).ViewEditMode = ViewEditMode.Edit;

            }
            
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void InvoiceListDCViewController_ListAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var cobj = ((DetailView)View).CurrentObject as InvoiceListDC;
            //IObjectSpace os = Application.CreateObjectSpace(typeof(Invoice));
            //var co = CriteriaOperator.Parse("StartDate >=? and EndDate <=?", cobj.StartDate, cobj.EndDate);
            //var wlist = os.GetObjects<Invoice>(co);

            //cobj.Details.Clear();

            //foreach (var item in wlist)
            //{
            //    InvoiceListDetailDC cdet = new InvoiceListDetailDC();
            //    cdet.Date = item.InvoiceDate;
            //    cdet.Item = os.GetObject(item.InvoiceItem.Name);
            //    cdet.Company = os.GetObject(item.Company.Name);
            //    if (item.Type != null)
            //    {
            //        cdet.Type = item.Type;
            //    }

            //    cobj.Details.Add(cdet);
            //}
            //View.Refresh();

            CompositeView dv = null;

            if (Frame is NestedFrame)
            {
                dv = ((NestedFrame)Frame).ViewItem.View;
            }

            var dvitem = dv.FindItem("ListItem");

            var co = CriteriaOperator.Parse("InvoiceDate >= ? and InvoiceDate <= ?", cobj.StartDate, cobj.EndDate);

            ((ListView)((DashboardViewItem)dvitem).InnerView).CollectionSource.Criteria["Filtre"] = co;

            ((DashboardViewItem)dvitem).Refresh();
        }
    }
}
