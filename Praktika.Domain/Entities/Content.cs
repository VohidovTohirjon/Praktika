using Praktika.Domain.Common;
using Praktika.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Praktika.Domain.Entities
{
    public class Content : IAuditable
    {
        public int Duration { get; set; }
        public string TypeOfCourse { get; set; }
        public ItemState State { get; set; }
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        [ForeignKey("Course")]
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
        public string Image { get; set; }

        public void Update()
        {
            UpdatedAt = DateTime.Now;
            State = ItemState.Updated;
        }

        public void Create()
        {
            CreatedAt = DateTime.Now;
            State = ItemState.Created;
        }

        public void Delete()
        {
            State = ItemState.Deleted;
        }
    }
}
