using PeopleViewer.Common;
using PersonDataReader.Service;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PeopleViewer.Presentation;

public class PeopleViewModel : INotifyPropertyChanged
{
    protected IPersonReader DataReader;

    private IEnumerable<Person> _people = [];

    public IEnumerable<Person> People
    {
        get => _people;
        set { _people = value; RaisePropertyChanged(); }
    }

    public PeopleViewModel(IPersonReader reader)
    {
        DataReader = reader;
    }

    public async Task RefreshPeople()
    {
        People = await DataReader.GetPeople();
    }

    public void ClearPeople()
    {
        People = [];
    }

    public string DataReaderType
    {
        get { return DataReader.GetTypeName(); }
    }


    #region INotifyPropertyChanged Members

    public event PropertyChangedEventHandler? PropertyChanged;
    private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion
}