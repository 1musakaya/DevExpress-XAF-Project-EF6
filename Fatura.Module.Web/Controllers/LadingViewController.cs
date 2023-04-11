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
using Fatura.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fatura.Module.Web.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class LadingViewController : ViewController
    {
        // Use CodeRush to create Controllers and Actions with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/403133/
        public LadingViewController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetObjectType = typeof(Lading);
            TargetViewType = ViewType.DetailView;
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            //Perform various tasks depending on the target View.


            var cobj = ((DetailView)View).CurrentObject as Lading;


            if (cobj != null)
            {

                if (ObjectSpace.IsNewObject(cobj))
                {
                    //cobj.Company  = 3434

                    var gobj = ObjectSpace.FindObject<Parameter>(CriteriaOperator.Parse("1=1"));
                    if (gobj != null)
                    {
                        cobj.Company = gobj.DefaultCompany;
                    }
                }
            }


            var pe = ((DetailView)View).FindItem("ExitAddress");
            if (pe != null)
            {
                ((PropertyEditor)pe).ControlValueChanged += LadingViewController_ExitAddressControlValueChanged;
            }
            var ad = ((DetailView)View).FindItem("ArrivalAddress");
            if (ad != null)
            {
                ((PropertyEditor)ad).ControlValueChanged += LadingViewController_ArrivalAddressControlValueChanged;
            }

            ObjectSpace.Committing += ObjectSpace_Committing;

            var ct = Frame.GetController<DeleteObjectsViewController>();

            if (ct != null)
            {
                ct.Deleting += Ct_Deleting;
            }

        }

        private void Ct_Deleting(object sender, DeletingEventArgs e)
        {
            //ObjectSpace.GetObjects<Invoice>(CriteriaOperator.Parse("Company=?", cobj));

            var obj = ((DetailView)View).CurrentObject as Lading;

            foreach (var item in obj.Details)
            {
                IList<InvoiceDetails> detList = ObjectSpace.GetObjects<InvoiceDetails>(CriteriaOperator.Parse("IncomeExpenseId=?", item.Id));

                if (detList != null)
                {
                    foreach (var ditem in detList)
                    {
                        ObjectSpace.Delete(ditem.Invoice);
                    }
                }
            }

            //ObjectSpace.CommitChanges();

        }

        private void LadingViewController_ArrivalAddressControlValueChanged(object sender, EventArgs e)
        {
            var ara = ((PropertyEditor)sender).ControlValue as Address;

            var obj = ((DetailView)View).CurrentObject as Lading;
            if (ara != null)
            {
                obj.ArrivalCountry = ara.Country;
                obj.ArrivalCity = ara.City;
            }
        }
        
        private void LadingViewController_ExitAddressControlValueChanged(object sender, EventArgs e)
        {
            var exa = ((PropertyEditor)sender).ControlValue as Address;
            var obj = ((DetailView)View).CurrentObject as Lading;
            if (exa != null)
            {
                obj.ExitCountry = exa.Country;
                obj.ExitCity = exa.City;
            }
        }

        private void ObjectSpace_Committing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var cobj = ((DetailView)View).CurrentObject;

            if (ObjectSpace.IsDeletedObject(cobj) || (ObjectSpace.IsDeleting))
            {
                return;
            }



            bool CheckArrival = false;
            bool CheckExit = false;
            var os = ((DetailView)View).ObjectSpace;

            var obj = ((DetailView)View).CurrentObject as Lading;

            var del = ObjectSpace.IsDeleting;

            if (!os.IsDeletedObject(obj))
            {

                if (obj.ArrivalAddress != null)
                {
                    CheckArrival = true;
                }

                if (obj.ExitAddress != null)
                {
                    CheckExit = true;
                }

                //if (!(CheckArrival || CheckExit))
                //{
                //    e.Cancel = true;
                //    throw new UserFriendlyException("Çıkış veya varış adresi zorunludur.!");
                //}


                //foreach (var item in obj.details)
                //{
                //     item.ıd -> ınvoicedetails.ıncomeexpenseıd

                //    var fobj = objectspace.findobject<ınvoicedetails>(criteriaoperator.parse("ıncomeexpenseıd=?", item.ıd));
                //    if (fobj != null)
                //    {
                //        e.cancel = true;
                //        throw new userfriendlyexception("faturası oluşturulmuş gelir/gider kaydı var. düzeltme yapılamaz!");
                //    }

                //}

            }

        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {


            var ad = ((DetailView)View).FindItem("ArrivalAddress");
            if (ad != null)
            {
                ((PropertyEditor)ad).ControlValueChanged -= LadingViewController_ArrivalAddressControlValueChanged;
            }

            var pe = ((DetailView)View).FindItem("ExitAddress");
            if (pe != null)
            {
                ((PropertyEditor)pe).ControlValueChanged -= LadingViewController_ExitAddressControlValueChanged;
            }

            View.ObjectSpace.Committing -= ObjectSpace_Committing;
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

    }
}
