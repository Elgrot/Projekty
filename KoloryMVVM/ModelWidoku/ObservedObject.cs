using System.ComponentModel;

public abstract class ObservedObject : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected void onPropertyChanged(params string[] nazwyWłasności)
    {
        if(PropertyChanged != null)
        {
            foreach(string nazwaWłasności in nazwyWłasności)
                PropertyChanged(this, new PropertyChangedEventArgs(nazwaWłasności));
        }
    }
}
