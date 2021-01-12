
using PAMApplication.MaturityModelContracts;
using PAMApplication.MaturityModelContracts.DTO;
using PAMDomain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAMApplication
{
    public class MaturityModelApplication
    {
        private IMaturityModelRepository MaturityModelRepo { get; set; }

        public MaturityModelApplication(IMaturityModelRepository maturityModelRepo)
        {
            this.MaturityModelRepo = maturityModelRepo;
        }

        public async Task<IList<MaturityModelListResponse>> ListMaturityModels()
        {
            List<MaturityModelListResponse> listMM = new List<MaturityModelListResponse>();

            foreach (var mm in (await MaturityModelRepo.List()))
            {
                listMM.Add(new MaturityModelListResponse
                {
                    MaturityModelId = mm.MaturityModelId,
                    Name = mm.Name,
                    Description = mm.Description,
                    Order = mm.Order,
                    Options = mm.Options.Select(o => new OptionsDTO { Name = o.Name, Value = o.Value, Level = o.Level}).ToList()
                });
            }

            return listMM;
        }
    }
}
