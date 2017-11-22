# .net Core Optional Dependencies

This project has a dependency tree that looks like:

OptionalDependency (Console App) => Dependency (Library) [ => OptionalSubDependency (Library)]

The output changes if you add a reference from the root project (OptionalDependency) to OptionalSubDependency.

This obviously lets you do cool things when used carefully (not type safe for really painfully obvious reasons).