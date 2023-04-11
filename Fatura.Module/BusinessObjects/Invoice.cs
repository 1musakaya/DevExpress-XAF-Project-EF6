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
using System.Runtime.InteropServices;
using System.Text;

namespace Fatura.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class Invoice : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        public Invoice()
        {
            Details = new List<InvoiceDetails>();
           
        }

        [VisibleInDetailView(false)]
        public int Id { get; protected set; }
        
        public virtual IncomeExpenseTypes Type { get; set; }
        public DateTime InvoiceDate { get; set; }
        [LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
        [DataSourceCriteria("Type = ##Enum#Fatura.Module.BusinessObjects.ActivePasiveTypes,Active#")]
        [RuleRequiredField]
        public virtual  Company Company { get; set; }
        [ImmediatePostData]
        public double DipTutar
        {
            get
            {
                double total = 0;
                foreach (var item in Details)
                {
                    total = total + (item.Price * item.Quantity);
                }
                return total;
            }
        }


        [Browsable(false)]
        [RuleFromBoolProperty("CheckType","Default",CustomMessageTemplate ="")]
        public bool CheckType
        {
            get
            {
                bool result = false;

                bool CheckIncome = false;
                bool CheckExpense = false;
                foreach (var item in Details)
                {
                    if (item.Items != null && (item.Items.InvoiceType == IncomeExpenseTypes.Income))
                    {
                        CheckIncome= true;
                    }
                    if (item.Items != null && (item.Items.InvoiceType == IncomeExpenseTypes.Expense))
                    {
                        CheckExpense= true;
                    }
                }
                result= !(CheckIncome || CheckExpense);
                return result;
            }
        }


        [Aggregated]
        public virtual IList<InvoiceDetails> Details { get; set; }



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
