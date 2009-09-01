using System;
using SpecMaker.Core;

namespace com.mosso.cloudfiles.unit.tests.Domain
{
    public class TestSpec: BaseSpec
    {
        
        public void when_is_not_working()
        {
            should("be working", ()=> { throw new Exception("testing exit codes"); });
        }
    }
}