using System.Collections.Generic;

public interface IPrioritiesEvaluator
{
    List<InteractionType> GetTargetPriorities();
}
