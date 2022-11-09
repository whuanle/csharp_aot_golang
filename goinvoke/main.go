package main

import "C"
import (
	"fmt"
	"syscall"
)

//export Start
func Start(a, b int) int {
	return a + b
}

func main() {
	maindll := syscall.NewLazyDLL("CsharpExport.dll")
	start := maindll.NewProc("Add")

	var a uintptr = uintptr(1)
	var b uintptr = uintptr(9)
	result, _, _ := start.Call(a, b)

	fmt.Println(result)
}
