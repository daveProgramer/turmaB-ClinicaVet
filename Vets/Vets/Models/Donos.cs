using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClinicaVeterinaria.Models
{
    public class Donos
    {

        // vai representar os dados da tabela dos DONOS

        // criar o construtor desta classe
        // e carregar a lista de Animais
        public Donos()
        {
            ListaDeAnimais = new HashSet<Animais>();
        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // impede o atributo de ser AutoNumber
        [Display(Name = "Identificador do Dono")]
        public int DonoID { get; set; }

        [Required(ErrorMessage = "Deve introduzir o {0}...")]
        [Display(Name = "Nome do Dono")]
        [RegularExpression("[A-Z][a-záéíóúàèìòùâêîôûãõäëïöüñç]+(( |'|-|( (de|das|dos|e) )|( d'))[A-Z][a-záéíóúàèìòùâêîôûãõäëïöüñç]+)*",
           ErrorMessage = "No '{0}' só pode usar letras. Cada palavra começa com uma letra maiúscula.")]
        public string Nome { set; get; }

        [Required(ErrorMessage ="Não se esqueça de preencher o nº do NIF")]
        [StringLength(9)]
        [RegularExpression("[0-9]{9}",
           ErrorMessage = "0 {0} Só aceita 9 algarismos")]
        public string NIF { get; set; }

        // especificar que um DONO tem muitos ANIMAIS
        public ICollection<Animais> ListaDeAnimais { get; set; }


    }
}