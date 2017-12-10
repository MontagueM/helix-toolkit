﻿using SharpDX.Direct3D11;
using System;
using System.Runtime.Serialization;

#if !NETFX_CORE
namespace HelixToolkit.Wpf.SharpDX.Shaders
#else
namespace HelixToolkit.UWP.Shaders
#endif
{
    using Utilities;

    [DataContract]
    public class ConstantBufferDescription : ICloneable
    {
        [DataMember]
        public string Name { set; get; }
        [DataMember]
        public int StructSize { set; get; }
        [DataMember]
        public int StrideSize { set; get; }
        [DataMember]
        public BindFlags BindFlags { set; get; } = BindFlags.ConstantBuffer;
        [DataMember]
        public CpuAccessFlags CpuAccessFlags { set; get; } = CpuAccessFlags.Write;
        [DataMember]
        public ResourceOptionFlags OptionFlags { set; get; } = ResourceOptionFlags.None;
        [DataMember]
        public ResourceUsage Usage { set; get; } = ResourceUsage.Dynamic;

        public ConstantBufferDescription() { }

        public ConstantBufferDescription(string name, int structSize, int strideSize=0)
        {
            Name = name;
            StructSize = structSize;
            StrideSize = strideSize;
        }

        public IBufferProxy CreateBuffer()
        {
            return new ConstantBufferProxy(this);
        }

        public ConstantBufferMapping CreateMapping(int slot)
        {
            return new ConstantBufferMapping(slot, this);
        }

        public object Clone()
        {
            return new ConstantBufferDescription(Name, StructSize, StrideSize)
            {
                BindFlags = this.BindFlags,
                CpuAccessFlags = this.CpuAccessFlags,
                OptionFlags = this.OptionFlags,
                Usage = this.Usage
            };
        }
    }

    [DataContract]
    public class ConstantBufferMapping : ICloneable
    {
        [DataMember]
        public int Slot { set; get; }
        [DataMember]
        public ConstantBufferDescription Description { set; get; }

        public ConstantBufferMapping(int slot, ConstantBufferDescription description)
        {
            Slot = slot;
            Description = description;
        }

        public static ConstantBufferMapping Create(int slot, ConstantBufferDescription description)
        {
            return new ConstantBufferMapping(slot, description);
        }

        public object Clone()
        {
            return new ConstantBufferMapping(this.Slot, (ConstantBufferDescription)this.Description.Clone());
        }
    }
}
