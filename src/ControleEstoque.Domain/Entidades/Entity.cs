using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estoque.Domain.Entidades
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }


        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(this, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }


        public static bool operator == (Entity a, Entity b)
        {

            if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;

            if ((ReferenceEquals(a, null) || ReferenceEquals(b, null))) return false;


            return a.Equals(b);
        }

        public static bool operator != (Entity a, Entity b)
        {
            return !(a == b);

        }

        // inpedindo de gerar um hashcode igual (Acontece por incrivel que pareça)

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }

        // Nome da entidade tal - Id Tal
        public override string ToString()
        {
            return $"{GetType().Name} [Id={Id}]";
        }


    }
}
