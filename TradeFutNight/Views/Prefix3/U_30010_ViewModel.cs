namespace TradeFutNight.Views.Prefix3
{
    public class U_30010_ViewModel : ViewModelParent<UIModel_30010>
    {
        public U_30010_ViewModel()
        {
        }
        public U_30010_A_ViewModel Vm_A
        {
            get { return GetProperty(() => Vm_A); }
            set { SetProperty(() => Vm_A, value); }
        }

        public U_30010_B_ViewModel Vm_B
        {
            get { return GetProperty(() => Vm_B); }
            set { SetProperty(() => Vm_B, value); }
        }

        public void Open()
        {
            Vm_A = new U_30010_A_ViewModel();
            Vm_A.Open();

            Vm_B = new U_30010_B_ViewModel();
            Vm_B.Open();
        }
    }

    public class UIModel_30010
    {
    }
}