using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Dima.Core.Requests.Categories
{
    public class UpdateCategoryRequest : BaseRequest
    {
        [JsonIgnore]
        public long Id { get; set; }
        [Required(ErrorMessage = "Título inválido")]
        [MaxLength(80, ErrorMessage = "O título debve conter até 80 caracteres")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "Descrição inválida")]
        public string? Description { get; set; }
    }
}