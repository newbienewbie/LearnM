
#[derive(Debug)]
pub enum MyError{
    ParseError(std::num::ParseIntError),
    TooSmall(TooSmall),
    TooLarge(TooLarge),
}


impl From<std::num::ParseIntError> for MyError
{
    fn from(value: std::num::ParseIntError) -> Self {
        MyError::ParseError(value)
    }
}



#[derive(Debug)]
pub struct TooSmall{
    pub input: i32,
    pub crit: f32
}
impl TooSmall {
    pub fn new(input: i32, crit: f32) -> TooSmall {
        TooSmall{ input, crit }
    }
}

impl From<TooSmall> for MyError
{
    fn from(value: TooSmall) -> Self {
        MyError::TooSmall(value)
    }
}




#[derive(Debug)]
pub struct TooLarge{
    pub input: i32,
    pub crit: f32
}
impl TooLarge {
    pub fn new(input: i32, crit: f32) -> TooLarge {
        TooLarge { input, crit }
    }
}
impl From<TooLarge> for MyError {
    fn from(value: TooLarge) -> Self {
        MyError::TooLarge(value)
    }
}

