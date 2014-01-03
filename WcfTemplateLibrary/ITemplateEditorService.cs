using System.Collections.Generic;
using System.ServiceModel;
using Cats.Service.TemplateEditor.Dto;

namespace Cats.Service.TemplateEditor
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITemplateEditorService" in both code and config file together.
    [ServiceContract]
    public interface ITemplateEditorService
    {
        [OperationContract]
        string TemplateTypes();

        [OperationContract]
        List<TemplateType> GetTemplateTypes();

        [OperationContract]
        List<Template> GetAllTemplates();

        [OperationContract]
        List<Template> GetTemplates(int templateType);

        [OperationContract]
        Template GetTemplateById(int templateId);

        [OperationContract]
        Template GetTemplateByName(string templateName);

        [OperationContract]
        List<TemplateFields> GetFieldsByTemplateId(int templateId);

    }
}
