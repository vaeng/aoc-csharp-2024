Register A: 51571418
Register B: 0
Register C: 0

Program:
2,4, bst RegB = RegA % 8
1,1, bxl RegB = RegB ^ 1
7,5, cdv RegC = RegA / 2 ** RegB
0,3, adv RegA = Reg A / 8
1,4, bxl RegB ^ 4
4,5, bxc Register B = RegB ^ Reg C = RegB ^4^RegC
5,5, output Register B =  RegB ^4^RegC
3,0 jnz // always jump to 0 if RegA != 0

16 Outputs ( 8 instructions times 2)
=> has to run 16 times.
=> RegA is divided by 8 every times
=> RegA >= 1 * 8^16 to 8^17 (1970324836974592)


Start:
 A = x
 B = 0
 C = 0


Every Iteration:
  B = A % 8
  B = B ^ 1
  C = A / Math.Pow(2, B)
  A = A / 8
  B = B ^ 4
  B = B ^ C
  Out: B % 8

Equal to:
  B = (A % 8) ^ 1
  C = (A / (long)Math.Pow(2, (A % 8) ^ 1))
  A = A / 8
  B = ((A % 8) ^ 1) ^ 4
  B = (((A % 8) ^ 1) ^ 4) ^ (A / (long)Math.Pow(2, (A % 8) ^ 1))
  Out: B % 8

Index:

Program:
2,4,1,1,7,5,0,3,1,4,4,5,5,5,3,0
Output B:
.,.,.,.,.,.,0,.,.,.,.,.,.,.,.,0
Lowest possible C:
.,.,.,.,.,.,0,.,.,.,.,.,.,.,.,0
Selecting B:
.,.,.,.,.,.,4,.,.,.,.,.,.,.,.,4
Resulting Shift of A:
.,.,.,.,.,.,2,.,.,.,.,.,.,.,.,2
Resulting A:
.,.,.,.,0,.,.,.,.,.,.,.,.,0,.,5

Program:
2,4,1,1,7,5,0,3,1,4,4,5,5,5,3,0
Output B:
.,.,.,.,.,.,0,.,1,.,.,.,.,.,.,0
Lowest possible C:
.,.,.,.,.,.,0,.,2,.,.,.,.,.,.,0
Selecting B:
.,.,.,.,.,.,4,.,3,.,.,.,.,.,.,4
Resulting Shift of A:
.,.,.,.,.,.,2,.,.,.,.,.,.,.,.,2
Resulting A:
.,.,.,.,0,.,.,.,.,.,.,.,.,0,.,5
