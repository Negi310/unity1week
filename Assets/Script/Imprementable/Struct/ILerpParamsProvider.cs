using UnityEngine;
using System.Collections.Generic;

public interface ILerpParamsProvider<T>
{
    List<LerpParams<T>> GetLerpParams();
}
