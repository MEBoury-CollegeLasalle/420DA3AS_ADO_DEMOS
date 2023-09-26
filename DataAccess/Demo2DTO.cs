using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPatates.DataAccess;
internal class Demo2DTO {
    private Demo1DTO? demo1Object;

    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int Demo1Id { get; set; }

    public Demo1DTO ObjetDemo1 { 
        get {
            // TODO: exemple de lazy loading (pas un vrai TODO)
            this.demo1Object ??= Demo1DAO.GetById(this.Demo1Id);
            return this.demo1Object;
        } 
        set { 
            this.demo1Object = value;
        } 
    }

}
