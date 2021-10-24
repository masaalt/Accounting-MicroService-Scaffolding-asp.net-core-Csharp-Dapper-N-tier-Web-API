using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Framework.Enum;
using Framework.ResultModel;
using IRepository.Accounting;
using Microsoft.AspNetCore.Mvc;
using NLog;
using ViewModel.Accounting.ViewModels;
using ViewModel.Accounting.ViewModels.Validator;

namespace AccountingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly Result _result;
        readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly ICountryRepository _countryRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="countryRepository"></param>
        /// <param name="result"></param>
        public CountryController(ICountryRepository countryRepository, Result result)
        {
            _countryRepository = countryRepository;
            _result = result;
        }


        /// <summary>
        /// مشاهده لیست کشور
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     api/countries
        ///     {
        ///     }
        ///
        /// </remarks>
        /// <returns>لیست کشور</returns>
        /// <response code="200">موفق</response>
        /// <response code="400">ناموفق</response>
        /// <response code="401">عدم دسترسی</response>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var data = await _countryRepository.Get();
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex, ex.Message);
                _result.Message = ex.Message;
                return BadRequest(_result);
            }
        }

        /// <summary>
        ///  درج کشور جدید
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     api/countries
        ///     {
        ///         "countryCode": "12345",
        ///         "countryName": "ایران",
        ///         "defaultCode": "12345",
        ///         "population": 1,
        ///         "area": 1,
        ///         "distance": 1,
        ///         "lenWid": 1,
        ///         "capital": "تست",
        ///         "time": "2019-08-05T18:54:26.276Z"
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">موفق</response>
        /// <response code="400">ناموفق</response>
        /// <response code="401">عدم دسترسی</response>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CountryViewModel countryViewModel)
        {
            try
            {
                var validator = new CountryValidator();
                var validationResult = validator.Validate(countryViewModel);
                if (validationResult.IsValid)
                {
                    int res = await _countryRepository.Add(countryViewModel);
                    if (res > 0)
                    {
                        return Ok(_result);
                    }
                    else
                    {
                        return BadRequest(_result);
                    }
                }
                else
                {
                    if (_result.Errors == null)
                        _result.Errors = new List<Error>();
                    foreach (var validationError in validationResult.Errors)
                        _result.Errors.Add(new Error()
                        {
                            Code = validationError.ErrorCode,
                            Field = validationError.PropertyName,
                            Title = "مقادیر ورودی معتبر نیستند",
                            Detail = validationError.ErrorMessage,
                        });
                    _result.Message = "مقادیر ورودی معتبر نیستند";
                    _result.Status = StatusType.BadRequest;
                    return BadRequest(_result);
                }

            }
            catch (Exception ex)
            {
                _logger.Fatal(ex, ex.Message);
                _result.Message = ex.Message;
                return BadRequest(_result);
            }
        }

        /// <summary>
        ///  ویرایش کشور
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     api/countries
        ///     {
        ///         "countryId": 1,
        ///         "countryCode": "12345",
        ///         "countryName": "ایران",
        ///         "defaultCode": "12345",
        ///         "population": 1,
        ///         "area": 1,
        ///         "distance": 1,
        ///         "lenWid": 1,
        ///         "capital": "تست",
        ///         "time": "2019-08-05T18:54:26.276Z"
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">موفق</response>
        /// <response code="400">ناموفق</response>
        /// <response code="401">عدم دسترسی</response>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] CountryViewModel countryViewModel)
        {
            try
            {
                var validator = new CountryValidator();
                var validationResult = validator.Validate(countryViewModel);
                if (validationResult.IsValid)
                {
                    int res = await _countryRepository.Update(countryViewModel);
                    if (res > 0)
                    {
                        return Ok(_result);
                    }
                    else
                    {
                        return BadRequest(_result);
                    }
                }
                else
                {
                    if (_result.Errors == null)
                        _result.Errors = new List<Error>();
                    foreach (var validationError in validationResult.Errors)
                        _result.Errors.Add(new Error()
                        {
                            Code = validationError.ErrorCode,
                            Field = validationError.PropertyName,
                            Title = "مقادیر ورودی معتبر نیستند",
                            Detail = validationError.ErrorMessage,
                        });
                    _result.Message = "مقادیر ورودی معتبر نیستند";
                    _result.Status = StatusType.BadRequest;
                    return BadRequest(_result);
                }

            }
            catch (Exception ex)
            {
                _logger.Fatal(ex, ex.Message);
                _result.Message = ex.Message;
                return BadRequest(_result);
            }
        }

        /// <summary>
        ///  حذف کشور
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     api/countries/1
        ///     {
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">موفق</response>
        /// <response code="400">ناموفق</response>
        /// <response code="401">عدم دسترسی</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                int res = await _countryRepository.Delete(id);
                if (res > 0)
                {
                    return Ok(_result);
                }
                else
                {
                    return BadRequest(_result);

                }

            }
            catch (Exception ex)
            {
                _logger.Fatal(ex, ex.Message);
                _result.Message = ex.Message;
                return BadRequest(_result);
            }
        }
    }
}