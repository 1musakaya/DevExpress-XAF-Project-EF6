    using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Fatura.Module.BusinessObjects
{
    public class InvoiceDetails : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        public InvoiceDetails()
        {
        }
        [Browsable(false)]  // Hide the entity identifier from UI.
        public int Id { get; protected set; }
        public double Quantity { get; set; }
        public double Price { get; set; }

        [LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
        [DataSourceCriteria("(Type = ##Enum#Fatura.Module.BusinessObjects.ActivePasiveTypes,Active#) and (InvoiceType = '@Invoice.Type')")]
        public virtual InvoiceItem Items { get; set; }
        [ImmediatePostData] 
        public double Total
        {
            get
            {
                return (Quantity* Price);
            }
        }
        [Browsable(false)]
        public int IncomeExpenseId { get ; set; }

        [Browsable(false)]
        public int InvoiceId;
        public virtual Invoice Invoice { get; set; }


        #region IXafEntityObject members (see https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppIXafEntityObjecttopic.aspx)
        void IXafEntityObject.OnCreated()
        {
            // Place the entity initialization code here.
            // You can initialize reference properties using Object Space methods; e.g.:
            // this.Address = objectSpace.CreateObject<Address>();
        }
        void IXafEntityObject.OnLoaded()
        {
            // Place the code that is executed each time the entity is loaded here.
        }
        void IXafEntityObject.OnSaving()
        {
            // Place the code that is executed each time the entity is saved here.
        }
        #endregion

        #region IObjectSpaceLink members (see https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppIObjectSpaceLinktopic.aspx)
        // Use the Object Space to access other entities from IXafEntityObject methods (see https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113707.aspx).
        private IObjectSpace objectSpace;
        IObjectSpace IObjectSpaceLink.ObjectSpace
        {
            get { return objectSpace; }
            set { objectSpace = value; }
        }
        #endregion

        #region INotifyPropertyChanged members (see http://msdn.microsoft.com/en-us/library/system.componentmodel.inotifypropertychanged(v=vs.110).aspx)
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
