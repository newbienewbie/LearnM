mod my_error;
mod proc;

use my_error::{MyError};


fn main(){
    let r = proc::rop();
    match r {
        Err(r) =>{
            match r {
                MyError::ParseError(r) => println!("错误：{}", r),
                MyError::TooSmall(reason) => println!("输入太小{}<{}", reason.input, reason.crit),
                MyError::TooLarge(reason) => println!("输入太大{}>{}", reason.input, reason.crit),
            }
        }
        Ok(z) => {
            println!("结果是{}", z);
        }
    }
}


