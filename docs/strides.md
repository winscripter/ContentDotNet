# Strides
ContentDotNet supports stride-based approach where, instead of reading the entire image, you read part of the stride (image line).

This essentially allows you to read/write while performing 0 bytes of heap allocation.
