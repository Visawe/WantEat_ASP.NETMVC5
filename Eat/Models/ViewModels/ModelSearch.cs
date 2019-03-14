using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eat.Models.ViewModels
{
    public class ModelSearch
    {
        public string Place { get; set; }
        public DateTime? Date { get; set; }
        public TimeSpan? Time { get; set; }
        public ushort? NumberPersons { get; set; }
        public string TextSearch { get; set; }
        public string SearchTarget { get; set; }

        public string ToStringForDish()
        {
            String result = String.Empty;
            if (Place != null)
            {
                result += "Место поиска: " + Place + ". ";
            }
            if (SearchTarget != null)
            {
                result += "Объект поиска: " + SearchTarget + ". ";
            }
            if (TextSearch != null)
            {
                result += "Текст поиска: " + TextSearch + ". ";
            }
            return (result);
        }

        public string ToStringForRest()
        {
            String result = String.Empty;
            if(Place != null)
            {
                result += "Место поиска: " + Place + ". ";
            }
            if(Date != null)
            {
                result += "Дата: " + Date.Value.Date.ToShortDateString() + ". ";
            }
            if (Time != null)
            {
                result += "Время: " + Time.Value.ToString() + ". ";
            }
            if (NumberPersons != null)
            {
                result += "Количество персон: " + NumberPersons + ". ";
            }
            return (result);
        }
    }
}