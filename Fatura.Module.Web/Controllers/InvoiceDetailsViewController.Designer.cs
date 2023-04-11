namespace Fatura.Module.Web.Controllers
{
    partial class InvoiceDetailsViewController
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.InvoiceDetailsViewController_CreateAction = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // InvoiceDetailsViewController_CreateAction
            // 
            this.InvoiceDetailsViewController_CreateAction.Caption = "Create Invoice";
            this.InvoiceDetailsViewController_CreateAction.ConfirmationMessage = null;
            this.InvoiceDetailsViewController_CreateAction.Id = "InvoiceDetailsViewController_CreateAction";
            this.InvoiceDetailsViewController_CreateAction.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireMultipleObjects;
            this.InvoiceDetailsViewController_CreateAction.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.InvoiceDetailsViewController_CreateAction.ToolTip = null;
            this.InvoiceDetailsViewController_CreateAction.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.InvoiceDetailsViewController_CreateAction.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.InvoiceDetailsViewController_CreateAction_Execute);
            // 
            // InvoiceDetailsViewController
            // 
            this.Actions.Add(this.InvoiceDetailsViewController_CreateAction);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction InvoiceDetailsViewController_CreateAction;
    }
}
