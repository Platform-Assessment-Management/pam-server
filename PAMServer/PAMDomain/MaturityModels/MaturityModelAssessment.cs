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
            var mmResult = new MaturityModelAssessmentResult();

            var maturityRepo = UtilDomain.GetService<IMaturityModelRepository>();

            var project = await maturityRepo.GetProjectAsync(projectId);
            var chapterIds = project.Chapters.ToList();

            var maturityModels = await maturityRepo.GetMaturitiesByChapters(chapterIds.ToArray());
            var camps = await maturityRepo.GetCampsByChapters(chapterIds.ToArray());

            foreach(var chapterId in chapterIds)
            {
                var campsByChap = camps.Where(camp => camp.Chapter.ChapterId == chapterId).ToList();

                foreach(var camp in campsByChap)
                {
                    var MMs = maturityModels.Where(m => camp.CampLevel.Exists(cl => cl.Key == m.MaturityModelId)).ToList();

                    var sumCamp = 0;
                    var currentCamp = 0;

                    foreach (var mm in MMs)
                    {
                        int maxOpt = 0;
                        int currentOpt = 0;

                        foreach (var options in mm.Options)
                        {
                            if (options.Level > maxOpt)
                                maxOpt = options.Level;
                        }

                        sumCamp += maxOpt;
                        currentCamp += currentOpt;

                        mmResult.SetMaturity(mm, currentOpt / maxOpt);
                    }

                    mmResult.SetCamp(camp, currentCamp / sumCamp);
                    
                }
            }

            return mmResult;
        }
    }
}
