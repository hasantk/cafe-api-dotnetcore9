using AutoMapper;
using FluentValidation;
using KafeAPI.Application.Dtos.ResponseDtos;
using KafeAPI.Application.Dtos.TableDtos;
using KafeAPI.Application.Interfaces;
using KafeAPI.Application.Services.Abstract;
using KafeAPI.Domain.Entities;

namespace KafeAPI.Application.Services.Concrete
{
    public class TableServices:ITableServices
    {
        private readonly IGenericRepository<Table> _tableRespository;
        private readonly ITableRespository _tableRespository1;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateTableDto> _createTableValidator;
        private readonly IValidator<UpdateTableDto> _updateTableValidator;

        public TableServices(IGenericRepository<Table> tableRespository, IMapper mapper, IValidator<CreateTableDto> createTableValidator, IValidator<UpdateTableDto> updateTableValidator, ITableRespository tableRespository1)
        {
            _tableRespository = tableRespository;
            _mapper = mapper;
            _createTableValidator = createTableValidator;
            _updateTableValidator = updateTableValidator;
            _tableRespository1 = tableRespository1;
        }

        public async Task<ResponseDto<object>> AddTable(CreateTableDto dto)
        {
            try
            {
                var validate = await _createTableValidator.ValidateAsync(dto);
                if (!validate.IsValid) 
                {
                    return new ResponseDto<object> { Success = false,Data=null, Message=string.Join(",",validate.Errors.Select(x=>x.ErrorMessage)),ErrorCode=ErrorCodes.ValidationError };
                }
                var checkTable = await _tableRespository1.GetByTableNumberAsync(dto.TableNumber);
                if (checkTable != null) 
                {
                    return new ResponseDto<object> { Success = false, Data = null, Message = "Eklemek İstediğiniz Masa Numarası Mevcuttur.", ErrorCode = ErrorCodes.DuplicateError };
                }
                var result = _mapper.Map<Table>(dto);
                await _tableRespository.AddAsync(result);
                return new ResponseDto<object> { Success = true, Data = null, Message = "Masa Başarılı Bir Şekilde Eklendi." };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object> { Success=false,Data=null,Message="Bir Sorun Oluştu.", ErrorCode=ErrorCodes.Exception};
            }
        }

        public async Task<ResponseDto<object>> DeleteTable(int id)
        {
            try

            {
                var rp = await _tableRespository.GetByIdAsync(id);
                if (rp == null) 
                {
                    return new ResponseDto<object> { Success = false, Data = null, Message = "Masa Bulunamadı.", ErrorCode = ErrorCodes.NotFound };
                }
                await _tableRespository.DeleteAsync(rp);
                return new ResponseDto<object> { Success = true, Data = null, Message = "Masa Başarılı Bir Şekilde Silindi." };
            }
            catch (Exception ex)
            {
                return new ResponseDto<Object> { Success = false, Data = null, Message = "Bir Hata Oluştu.", ErrorCode = ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<List<ResultTableDto>>> GetAllActiveTablesGeneric()
        {
            try
            {
                var rp = await _tableRespository.GetAllAsync();
                rp = rp.Where(x => x.IsActive == true).ToList();
                if (rp.Count == 0)
                {
                    return new ResponseDto<List<ResultTableDto>> { Success = false, Data = null, Message = "Masalar Bulunamadı.", ErrorCode = ErrorCodes.NotFound };
                }

                var result = _mapper.Map<List<ResultTableDto>>(rp);
                return new ResponseDto<List<ResultTableDto>> { Success = true, Data = result };
            }
            catch (Exception ex)
            {
                return new ResponseDto<List<ResultTableDto>> { Success = false, Data = null, Message = "Bir Hata Oluştu.", ErrorCode = ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<List<ResultTableDto>>> GetAllActiveTables()
        {
            try
            {
                var rp = await _tableRespository1.GetAllActiveTablesAsync();
                rp = rp.Where(x => x.IsActive == true).ToList();
                if (rp.Count == 0)
                {
                    return new ResponseDto<List<ResultTableDto>> { Success = false, Data = null, Message = "Masalar Bulunamadı.", ErrorCode = ErrorCodes.NotFound };
                }

                var result = _mapper.Map<List<ResultTableDto>>(rp);
                return new ResponseDto<List<ResultTableDto>> { Success = true, Data = result };
            }
            catch (Exception ex)
            {
                return new ResponseDto<List<ResultTableDto>> { Success = false, Data = null, Message = "Bir Hata Oluştu.", ErrorCode = ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<List<ResultTableDto>>> GetAllTables()
        {
            try
            {
                var rp = await _tableRespository.GetAllAsync();
                if (rp.Count == 0) 
                {
                    return new ResponseDto<List<ResultTableDto>> { Success = false, Data = null, Message = "Masalar Bulunamadı.", ErrorCode = ErrorCodes.NotFound };
                }

                var result = _mapper.Map<List<ResultTableDto>>(rp);
                return new ResponseDto<List<ResultTableDto>> { Success = true, Data = result };
            }
            catch (Exception ex)
            {
                return new ResponseDto<List<ResultTableDto>> { Success = false, Data = null, Message = "Bir Hata Oluştu.", ErrorCode = ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<DetailTableDto>> GetByIdTable(int id)
        {
            try
            {
                var rp = await _tableRespository.GetByIdAsync(id);
                if (rp == null)
                {
                    return new ResponseDto<DetailTableDto> { Success = false, Data = null, Message = "Masa Bulunamadı.", ErrorCode = ErrorCodes.NotFound };
                }
                var result = _mapper.Map<DetailTableDto>(rp);
                return new ResponseDto<DetailTableDto> { Success = true, Data =result};
            }
            catch (Exception ex)
            {
                return new ResponseDto<DetailTableDto> { Success = false, Data = null, Message = "Bir Sorun Oluştu.", ErrorCode = ErrorCodes.Exception};
            }
        }

        public async Task<ResponseDto<DetailTableDto>> GetByTableNumber(int tableNumber)
        {
            try
            {
                var table = await _tableRespository1.GetByTableNumberAsync(tableNumber);
                if (table == null) 
                {
                    return new ResponseDto<DetailTableDto> { Success = false, Data = null, Message = "Masa Bulunamadı.", ErrorCode = ErrorCodes.NotFound };
                }
                var result = _mapper.Map<DetailTableDto>(table);
                return new ResponseDto<DetailTableDto> {Success = true, Data = result};
            }
            catch (Exception ex)
            {
                return new ResponseDto<DetailTableDto> { Success = false, Data = null, Message = "Bir Hata Oluştu.", ErrorCode = ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<object>> UpdateTable(UpdateTableDto dto)
        {
            try
            {
                var validate = await _updateTableValidator.ValidateAsync(dto);
                if (!validate.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message = String.Join(",", validate.Errors.Select(x => x.ErrorMessage)),
                        ErrorCode = ErrorCodes.ValidationError
                    };
                }
                var rp = await _tableRespository.GetByIdAsync(dto.Id);
                if (rp == null) 
                {
                    return new ResponseDto<object> { Success = false, Data = null, Message = "Masa Bulunamadı.", ErrorCode = ErrorCodes.NotFound };
                }
                var result = _mapper.Map(dto, rp);
                await _tableRespository.UpdateAsync(result);
                return new ResponseDto<object> { Success = true, Data = null, Message = "Masa Başarılı Bir Şekilde Güncellendi." };
            }
            catch (Exception ex)
            {
                return new ResponseDto<Object> { Success = false, Data = null, Message = "Bir Hata Oluştu.", ErrorCode = ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<object>> UpdateTableStatusById(int id)
        {

            try
            {
                var rp = await _tableRespository.GetByIdAsync(id);
                if (rp == null)
                {
                    return new ResponseDto<object> { Success = false, Data = null, Message = "Masa Bulunamadı.", ErrorCode = ErrorCodes.NotFound };
                }
                rp.IsActive = !rp.IsActive;
                await _tableRespository.UpdateAsync(rp);
                return new ResponseDto<object> { Success = true, Data = null, Message = "Masa Başarılı Bir Şekilde Güncellendi." };
            }
            catch (Exception ex)
            {
                return new ResponseDto<Object> { Success = false, Data = null, Message = "Bir Hata Oluştu.", ErrorCode= ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<object>> UpdateTableStatusByTableNumber(int tableNumber)
        {
            try
            {
                var rp = await _tableRespository1.GetByTableNumberAsync(tableNumber);
                if (rp == null)
                {
                    return new ResponseDto<object> { Success = false, Data = null, Message = "Masa Bulunamadı.", ErrorCode = ErrorCodes.NotFound };
                }
                rp.IsActive = !rp.IsActive;
                await _tableRespository.UpdateAsync(rp);
                return new ResponseDto<object> { Success = true, Data = null, Message = "Masa Başarılı Bir Şekilde Güncellendi." };
            }
            catch (Exception ex)
            {
                return new ResponseDto<Object> { Success = false, Data = null, Message = "Bir Hata Oluştu.", ErrorCode = ErrorCodes.Exception };
            }
        }
    }
}
