using PAMDomain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAMDomain.MaturityModels
{
    public class MaturityModelAssessment
    {
        public static async Task<MaturityModelAssessmentResult> ComputeAsync(Guid projectId)
        {
            var result = new MaturityModelAssessmentResult();
            var mmResult = new MaturityModelAssessmentResult();

            var maturityRepo = UtilDomain.GetService<IMaturityModelRepository>();

            var project = await maturityRepo.GetProjectAsync(projectId);

            var chapterIds = project.ChaptersIds.ToArray();
            var chapters = await maturityRepo.GetChaptersAsync(chapterIds);
            var camps = await maturityRepo.GetCampsByChapters(chapterIds);

            var maturityModels = await maturityRepo.GetAsync(camps.Where(c => c.CampLevel != null).SelectMany(c => c.CampLevel.Select(cl => cl.Key)).ToArray());

            foreach (var chapter in chapters)
            {
                var chapterResult = result.AddChapters(chapter);

                foreach (var camp in camps)
                {
                    var campResult = chapterResult.AddCamps(camp);

                    foreach(var mm in maturityModels.Where(m => camp.ContainsMaturity(m.MaturityModelId)))
                    {
                        campResult.Validade(mm, project);
                    }
                }
            }

            return result;
        }
    }
}
