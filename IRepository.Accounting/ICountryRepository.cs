using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModel.Accounting.ViewModels;

namespace IRepository.Accounting
{
    public interface ICountryRepository
    {
        Task<List<CountryViewModel>> Get();
        Task<int> Add(CountryViewModel model);
        Task<int> Update(CountryViewModel model);
        Task<int> Delete(int id);
    }
}
