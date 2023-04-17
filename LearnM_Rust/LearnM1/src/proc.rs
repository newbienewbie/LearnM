use std::io;

use crate::my_error::{MyError, TooSmall, TooLarge};


pub fn rop() -> Result<i32, MyError>{
    let x = input_int("输入a:")?;
    require_gt(x, 30.0)?;
    let y = input_int("输入b:")?;
    require_lt(y, 100.0)?;
    let z = x+ y;
    Ok(z)
}



fn input_int(tip: &str) -> Result<i32, MyError>{
    println!("{}",tip);
    let mut user_input = String::new();
    let stdin = io::stdin();
    let _ = stdin.read_line(&mut user_input).unwrap();
    println!("你输入了{}",user_input);

    let x = user_input.trim().parse::<i32>()?;
    Ok(x)
}

fn require_gt(input: i32, crit: f32) -> Result<(), TooSmall>
{
    let finput = input as f32;
    if finput <= crit {
        Err(TooSmall::new(input, crit))
    }
    else {
        Ok(())
    }
}

fn require_lt(input: i32, crit: f32) -> Result<(), TooLarge>
{
    let finput = input as f32;
    if finput >= crit {
        Err(TooLarge::new(input, crit))
    }
    else {
        Ok(())
    }
}
