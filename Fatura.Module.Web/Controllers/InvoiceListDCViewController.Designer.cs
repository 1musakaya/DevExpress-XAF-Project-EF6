namespace Fatura.Module.Web.Controllers
{
    partial class InvoiceListDCViewController
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
            this.InvoiceListDCViewController_ListAction = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // InvoiceListDCViewController_ListAction
            // 
            this.InvoiceListDCViewController_ListAction.Caption = "List";
            this.InvoiceListDCViewController_ListAction.Category = "InvoiceListActionCategory";
            this.InvoiceListDCViewController_ListAction.ConfirmationMessage = null;
            this.InvoiceListDCViewController_ListAction.Id = "InvoiceListDCViewController_ListAction";
            this.InvoiceListDCViewController_ListAction.ToolTip = null;
            this.InvoiceListDCViewController_ListAction.TypeOfView = typeof(DevExpress.ExpressApp.View);
            this.InvoiceListDCViewController_ListAction.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.InvoiceListDCViewController_ListAction_Execute);
            // 
            // InvoiceListDCViewController
            // 
            this.Actions.Add(this.InvoiceListDCViewController_ListAction);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction InvoiceListDCViewController_ListAction;
    }
}
