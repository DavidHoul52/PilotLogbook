using System;
using System.ComponentModel;
using System.Linq.Expressions;
using LogbookApp.Views;

namespace LogbookApp.ViewModel
{
    public class ViewModelBase :INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged<TViewModel>(Expression<Func<TViewModel>> property)
        {
            var expression = property.Body as MemberExpression;
            if (expression != null)
            {
                var member = expression.Member;

                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(member.Name));
                }
            }
        }

        public Action GoBack { get; set; }

        public IMessager Messager { get; set; }
    }
}
