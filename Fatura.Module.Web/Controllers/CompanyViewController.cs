 using DevExpress.Charts.Native;
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
using DevExpress.Utils.CodedUISupport;
using Fatura.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fatura.Module.Web.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class CompanyViewController : ViewController
    {
        // Use CodeRush to create Controllers and Actions with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/403133/
        public CompanyViewController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetObjectType = typeof(Company);
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            ObjectSpace.Committing += ObjectSpace_Committing;

            var ft = Frame.GetController<DeleteObjectsViewController>();
            if (ft != null)
            {
                ft.Deleting += Ft_Deleting;
            }
        }

        private void Ft_Deleting(object sender, DeletingEventArgs e)
        {
            
            var obj = ((DetailView)View).CurrentObject as Company;
            var inv = ObjectSpace.GetObjects<Invoice>(CriteriaOperator.Parse("Company=?", obj));
            if (inv.Count > 0)
            {
                e.Cancel = true;
                throw new UserFriendlyException("Company bir faturada veya yükte kullanıldığı için silinemez.");
            }
            
            var lad = ObjectSpace.GetObjects<Lading>(CriteriaOperator.Parse("Company=?", obj));
            if (lad.Count > 0)
            {
                e.Cancel = true;
                throw new UserFriendlyException("Company bir faturada veya yükte kullanıldığı için silinemez.");
            }
            //else 
            //{
            //    ObjectSpace.Delete(obj);
                
            //}
            //if (inv.Count < 0 && lad.Count < 0)
            //{
            //    ObjectSpace.Delete(obj);
            //}
            
        }

        private void ObjectSpace_Committing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var cobj = ((DetailView)View).CurrentObject;

            if (ObjectSpace.IsDeletedObject(cobj) || (ObjectSpace.IsDeleting))
            {
                return;
            }
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            ObjectSpace.Committing -= ObjectSpace_Committing;
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
