using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.Validation;
using DevExpress.XtraReports.Design.ParameterEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Fatura.Module.BusinessObjects
{
    [DefaultClassOptions]
    [DefaultProperty("Id")]
    
    public class Lading : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        public Lading()
        {
            Details = new List<IncomeExpense>();
        }
        [Browsable(false)]  // Hide the entity identifier from UI.
        public int Id { get; protected set; }
        [RuleRequiredField]
        public virtual Company Company { get; set; }
        private Address exitAddress;
        [ImmediatePostData]
        public virtual Address ExitAddress 
        { 
            get
            {
                return exitAddress;
            } 
            set
            {
                exitAddress = value; OnPropertyChanged("ExitAddress");
            }
        }
        private Address arrivalAddress;
        [ImmediatePostData]
        public virtual Address ArrivalAddress
        { 
            get { return arrivalAddress; }
            set { arrivalAddress = value; OnPropertyChanged("ArrivalAddress"); }
        }
        public virtual Country ExitCountry { get; set; }
        public virtual City ExitCity { get; set; }
        public virtual Country ArrivalCountry { get; set; }
        public virtual City ArrivalCity { get; set; }

        public double TotalIncome
        {
            get
            {
                double total = 0;
                foreach (var item in Details)
                {
                    if (item.Type == IncomeExpenseTypes.Income)
                    {
                        total += (item.Quantity * item.Amount);
                    }

                }
                return total;
            }
        }
        public double TotalExpense
        {
            get
            {
                double total = 0;
                foreach (var item in Details)
                {
                    if (item.Type == IncomeExpenseTypes.Expense)
                    {
                        total += (item.Quantity * item.Amount);
                    }
                }
                return total;
            }
        }

        //[RuleFromBoolProperty("CheckIncomeExpense", "Save", CustomMessageTemplate = "Lütfen tek bir türden Fatura oluşturunuz.")]
        //public bool CheckIncomeExpense
        //{
        //    get
        //    {
        //        bool result = false;

        //        bool checkIncome = false;
        //        bool checkExpense = false;

        //        foreach (var item in Details)
        //        {
        //            if ((item.Type != null) && (item.Type == IncomeExpenseTypes.Income))
        //            {
        //                checkIncome = true;
        //            }
        //            if ((item.Type != null) && (item.Type == IncomeExpenseTypes.Expense))
        //            {
        //                checkExpense = true;
        //            }
        //        }
        //        result = (checkIncome && checkExpense);

        //        return result;
        //    }
        //}
        [Aggregated]
        public virtual IList<IncomeExpense> Details { get; set; }

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
