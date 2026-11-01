global using Microsoft.AspNetCore.Mvc;
#if (useEf)
global using Microsoft.EntityFrameworkCore;
#endif
#if (useMapper)
global using Riok.Mapperly.Abstractions;
#endif

// Internal libraries
global using SharedKernel.Extensions;