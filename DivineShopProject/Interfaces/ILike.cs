using DivineShopProject.Models;
using DivineShopProject.View_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DivineShopProject.Interfaces
{
    public interface ILike
    {
        Like GetLike(String username, int id);
        void AddLike(Like like);
        IEnumerable<Like> GetAllLike(String username);
        void DisLike(String username, int id);

    }
}
