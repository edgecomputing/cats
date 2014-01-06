using System;
using System.Collections.Generic;
using System.Linq;
using Cats.Service.TemplateEditor.Dto;
using Cats.Services.EarlyWarning;

namespace Cats.Service.TemplateEditor
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TemplateEditorService" in both code and config file together.
    public class TemplateEditorService : ITemplateEditorService
    {
        private ITemplateService _templateService=new TemplateService();
        private ITemplateTypeTypeService _templateTypeService = new TemplateTypeService();
        private ITemplateFieldsService _templateFieldsService = new TemplateFieldService();

        public string TemplateTypes()
        {
            return "this is a sample string ";
        }

        public List<TemplateType> GetTemplateTypes()
        {
            var templatesTypes = _templateTypeService.GetAllTemplateType();
            var dtos = from t in templatesTypes
                       select new TemplateType
                       {
                           TemplateTypeId = t.TemplateTypeId,
                           TemplateObject = t.TemplateObject,
                           Remark = t.Remark
                       };
            return dtos.ToList();
        }

        public List<Template> GetAllTemplates()
        {
            var templates = _templateService.GetAllTemplate();
            var dtos = from t in templates
                       select new Template
                                  {
                                      TemplateId = t.TemplateId, 
                                      Name = t.Name,
                                      TemplateType = t.TemplateType1.TemplateTypeId,
                                      Remark = t.Remark
                                  };
            return dtos.ToList();
        }

        public List<Template> GetTemplates(int templateId)
        {
            var templates = _templateService.GetAllTemplate();
            var dtos = (from t in templates
                        where t.TemplateType1.TemplateTypeId == templateId
                        select new Template
                        {
                            TemplateId = t.TemplateId,
                            Name = t.Name,
                            TemplateType = t.TemplateType1.TemplateTypeId,
                            Remark = t.Remark
                        });
            return dtos.ToList();

           

        }

        public Template GetTemplateById(int templateId)
        {
            var templates = _templateService.FindById(templateId);
            var dtoTemplate = new Template()
                                  {
                                      TemplateId = templates.TemplateId,
                                      Name = templates.Name,
                                      TemplateType = templates.TemplateType1.TemplateTypeId,
                                      Remark = templates.Remark
                                  };

            return dtoTemplate;
        }

        public Template GetTemplateByName(string templateName)
        {
            throw new NotImplementedException();
        }

        public List<TemplateFields> GetFieldsByTemplateId(int templateId)
        {
            var templateFields = _templateFieldsService.GetAllTemplateField();
            var templateFieldsDto = from field in templateFields
                                    where field.TemplateId == templateId
                                    select new TemplateFields
                                               {
                                                   TemplateFieldId = field.TemplateFieldId,
                                                   Field = field.Field,
                                                   TemplateId = field.TemplateId,
                                               };
            return templateFieldsDto.ToList();


        }
    }
}
