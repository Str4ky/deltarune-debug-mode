#!/bin/bash

cd /home/gabio/drfr/deltatest/debugmode
~/drfr/reset_deltarune ; python3 patcher.py && python3 apply.py && ~/drfr/launch_deltarune
