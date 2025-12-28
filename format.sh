#!/bin/bash

DIAGNOSTICS="IDE0130";

dotnet format CustomCADs.slnx --exclude-diagnostics "$DIAGNOSTICS"