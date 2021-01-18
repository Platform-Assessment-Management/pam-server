
using PAMApplication.CampContracts;
using PAMApplication.Exceptions;
using PAMApplication.MaturityModelContracts;
using PAMApplication.MaturityModelContracts.DTO;
using PAMDomain;
using PAMDomain.MaturityModels;
using PAMDomain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAMApplication
{
    public class MaturityModelApplication
    {
        private IMaturityModelRepository maturityModelRepo { get; set; }

        public MaturityModelApplication(IMaturityModelRepository maturityModelRepo)
        {
            this.maturityModelRepo = maturityModelRepo;
        }

        public async Task<IList<MaturityModelListResponse>> ListMaturityModels()
        {
            List<MaturityModelListResponse> listMM = new List<MaturityModelListResponse>();

            foreach (var mm in (await maturityModelRepo.ListAsync()))
            {
                listMM.Add(new MaturityModelListResponse
                {
                    MaturityModelId = mm.MaturityModelId,
                    Name = mm.Name,
                    Description = mm.Description,
                    Order = mm.Order,
                    Options = mm.Options != null ? mm.Options.Select(o => new OptionsDTO { Name = o.Name, Value = o.Value, Level = o.Level}).ToList() : new List<OptionsDTO>()
                });;
            }

            return listMM;
        }

        public async Task<CampListResponse> ListCampAsync()
        {
            var camps = await this.maturityModelRepo.GetCampsAsync();

            return new CampListResponse
            {
                Camps = camps.Select(c => new CampListDTO
                {
                    CampId = c.CampId,
                    Name = c.Name,
                    Description = c.Description,
                    Order = c.Order,
                    ChapterId = c.ChapterId
                }).ToList()
            };
        }

        public async Task AlterCampAsync(CampAlterRequest request)
        {
            var camp = (await this.maturityModelRepo.GetCampsAsync(request.CampId)).FirstOrDefault();

            if (camp == null)
                throw new CampNotFoundException(request.CampId);

            await camp.AlterAsync(request.Name, request.Description, request.Order, request.ChapterId);
        }

        public async Task<CampCreateResponse> CreateCampAsync(CampCreateRequest request)
        {
            var camp = await CampDomain.CreateAsync(request.Name, request.Description, request.Order, request.ChapterId);

            return new CampCreateResponse
            {
                CampId = camp.CampId
            };
        }

        public async Task IncludeOption(MaturityModelIncludeOptionRequest request)
        {
            var maturityRepo = UtilDomain.GetService<IMaturityModelRepository>();

            var mm = await maturityRepo.GetAsync(request.MaturityId);

            if (mm == null)
                throw new MaturityModelNotFoundException(request.MaturityId);

            await mm.IncludeOptionAsync(request.Name, request.Value, request.Level);
        }

        public async Task<MaturityModelCreateResponse> CreateMaturity(MaturityModelCreateRequest request)
        {
            var mm = await MaturityModelDomain.CreateAsync(request.Name, request.Description, request.Order);
            return new MaturityModelCreateResponse
            {
                MaturityModelId = mm.MaturityModelId
            };
        }

        public async Task AlterOptionAsync(MaturityModelAlterOptionRequest request)
        {
            var maturityRepo = UtilDomain.GetService<IMaturityModelRepository>();

            var mm = await maturityRepo.GetAsync(request.MaturityId);

            if (mm == null)
                throw new MaturityModelNotFoundException(request.MaturityId);

            await mm.AlterOptionAsync(request.value, request.Name, request.Level);
        }

        public async Task Assesment(Guid projectId)
        {
            await MaturityModelAssessment.ComputeAsync(projectId);
        }
    }
}
