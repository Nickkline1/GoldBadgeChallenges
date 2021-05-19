using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claims
{
    public class Claim_Repo
    {
        private Queue<Claim> _repo = new Queue<Claim>();
        
        
        //CREATE
        public bool AddClaim (Claim claim)
        {
            int startingCount = _repo.Count;
            _repo.Enqueue(claim);
            bool wasAdded = (_repo.Count > startingCount) ? true : false;
            return wasAdded;
        }
        //READ
        public Queue<Claim> GetAllClaims()
        {
            return _repo;
        }

        //RETURN BUT DONT REMOVE
        public Claim PeekClaim()
        {
            if (_repo.Peek() != null)
            {
                return _repo.Peek();
            }
            return null;
        }
        //UPDATE- not needed for this challenge
        //DELETE
        public bool DequeueClaim()
        {
            int startingCount = _repo.Count;
            _repo.Dequeue();
            bool wasDequeued = (_repo.Count < startingCount) ? true : false;
            return wasDequeued;
        }
    }
}
