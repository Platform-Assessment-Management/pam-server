using System;
using System.Collections.Generic;
using System.Linq;

namespace PAMDomain.MaturityModels
{
    public class MaturityModelAssessmentResult
    {
        public List<ChaptersResult> Chapters { get; set; } = new List<ChaptersResult>();

        public ChaptersResult AddChapters(ChapterDomain chapterDomain)
        {
            var chapter = ChaptersResult.Create(chapterDomain);
            Chapters.Add(chapter);

            return chapter;
        }
    }

    public class ChaptersResult
    {
        private ChaptersResult() { }

        public static ChaptersResult Create(ChapterDomain chapterDomain)
        {
            return new ChaptersResult
            {
                ChapterId = chapterDomain.ChapterId,
                Name = chapterDomain.Name
            };
        }

        public Guid ChapterId { get; set; }

        public String Name { get; set; }

        public List<CampsResult> Camps { get; set; } = new List<CampsResult>();

        public Guid? CurrentCamp
        {
            get
            {
                CampsResult camp = null;
                
                foreach(var c in Camps)
                {
                    if (c.Approved)
                        camp = c;
                    else
                        break;
                }

                return camp != null ? (Guid?) camp.CampId : null;
            }
        }

        public CampsResult AddCamps(CampDomain camp)
        {
            var campResult = CampsResult.Create(camp);

            Camps.Add(campResult);

            return campResult;
        }
    }

    public class CampsResult
    {
        public static CampsResult Create(CampDomain camp)
        {
            return new CampsResult
            {
                Camp = camp,
                CampId = camp.CampId,
                Name = camp.Name,
                Order = camp.Order
            };
        }

        public void Validade(MaturityModelDomain mm, MaturityModelProjectDomain project)
        {
            var maturity = MaturityModelsResult.Create(Camp.LevelOfMaturity(mm.MaturityModelId), mm, project.ValueOf(mm.MaturityModelId));

            MaturityModel.Add(maturity);
        }

        private CampsResult() { }

        private CampDomain Camp { get; set; }

        public Guid CampId { get; set; }

        public String Name { get; set; }

        public int Order { get; set; }

        public bool Approved
        {
            get
            {
                return MaturityModel.All(m => m.Approved);
            }
        }

        public List<MaturityModelsResult> MaturityModel { get; set; } = new List<MaturityModelsResult>();
    }

    public class MaturityModelsResult
    {
        private MaturityModelsResult() { }

        public static MaturityModelsResult Create(float campLevel, MaturityModelDomain mm, int option)
        {
            var result = new MaturityModelsResult
            {
                MaturityModelId = mm.MaturityModelId,
                Name = mm.Name,
                MaxLevel = mm.MaxLevel(),
                CurrenteLevel = mm.LevelOf(option),
                MinimalLevel = campLevel
            };

            
            return result;
        }

        public Guid MaturityModelId { get; set; }
        public String Name { get; set; }
        public int MaxLevel { get; set; }
        public int CurrenteLevel { get; set; }
        public double Level
        {
            get
            {
                return CurrenteLevel / MaxLevel * 100.0;
            }
        }
        public double MinimalLevel { get; set; }
        public bool Approved
        {
            get
            {
                return Level >= MinimalLevel;
            }
        }
    }
}
