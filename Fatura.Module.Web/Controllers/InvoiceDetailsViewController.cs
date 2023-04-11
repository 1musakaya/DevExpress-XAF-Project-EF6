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
using DevExpress.Persistent.Validation;
using DevExpress.Utils.DirectXPaint;
using DevExpress.XtraCharts;
using Fatura.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fatura.Module.Web.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class InvoiceDetailsViewController : ViewController
    {
        // Use CodeRush to create Controllers and Actions with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/403133/
        public InvoiceDetailsViewController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetObjectType = typeof(IncomeExpense);
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
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

        private void InvoiceDetailsViewController_CreateAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            bool checkIncome = false;
            bool checkExpense = false;
            foreach (IncomeExpense item in e.SelectedObjects)
            {
                if ((item.Type != null) && (item.Type == IncomeExpenseTypes.Income))
                {
                    checkIncome = true;
                }
                if ((item.Type != null) && (item.Type == IncomeExpenseTypes.Expense))
                {
                    checkExpense = true;
                }

            }

            if ((checkIncome && checkExpense))
            {

                throw new UserFriendlyException("Lütfen tek bir türden fatura kaydı yapınız.");

            }

            else
            {
                IObjectSpace os = Application.CreateObjectSpace(typeof(Invoice));
                var obj = ((PropertyCollectionSource)((ListView)View).CollectionSource).MasterObject as Lading;

                var newobj = os.CreateObject<Invoice>();
                newobj.Company = os.GetObject(obj.Company);

                newobj.Type = ((IncomeExpense)e.SelectedObjects[0]).Type;

                foreach (IncomeExpense item in e.SelectedObjects)
                {
                    var dobj = os.CreateObject<InvoiceDetails>();
                    dobj.IncomeExpenseId = item.Id;
                    dobj.Quantity = item.Quantity;
                    dobj.Price = item.Amount;
                    dobj.Items = item.InvoiceItem;
                    newobj.Details.Add(dobj);
                }


                //os.CommitChanges();

                DetailView dv = Application.CreateDetailView(os, newobj);
                dv.ViewEditMode = ViewEditMode.Edit;

                e.ShowViewParameters.CreatedView = dv;
                e.ShowViewParameters.TargetWindow = TargetWindow.Default;
            }

        }
    }
}
