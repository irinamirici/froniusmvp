﻿using System;

namespace FroniusTests {
    public static class TestExtensions {
        public static void Repeat(this int count, Action action) {
            for (int x = 0; x < count; x += 1) {
                action();
            }
        }
    }
}
