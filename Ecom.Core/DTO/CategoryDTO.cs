using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.DTO
{
    public class CategoryDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }

        public CategoryDTO() { }

        public CategoryDTO(string name, string description)
        {
            Name = name;
            Description = description;
        }

       
    }

    public class UpdateCategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public UpdateCategoryDTO()
        {

        }

        public UpdateCategoryDTO(string name, string description, int id)
        {
            Name = name;
            Description = description;
            Id = id;
        }


    }
}
