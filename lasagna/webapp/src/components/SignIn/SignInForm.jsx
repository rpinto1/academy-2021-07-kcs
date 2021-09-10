import React, { useEffect, useState } from 'react';
import { Redirect } from 'react-router-dom';
import { Button, Checkbox, Container, Form } from 'semantic-ui-react';



export default function SignInForm() {

    const [user, setUser] = useState({
      EmailAddress: '',
      Password: '', 
    });

    const [loggedUser, setLoggedUser] = useState({
        id: '',
        token: ''
    });

   const [redirect, setRedirect] = useState(false);
   const [rememberMe, setRememberMe] = useState(false);
   const [keepMeLogged, setKeepMeLogged] = useState(false);

    const handleChange = (event) => {
      const { id, value } = event.target
      
      setUser(prevState => ({
          ...prevState,
          [id]: value     
      }));
      };
     
  const handleRememberMe = (event) => {
        setRememberMe(!rememberMe)
          if(rememberMe) {
          localStorage.setItem('username', user.EmailAddress.toString());
          localStorage.setItem('password', user.Password.toString());
      }
    };  
  
  const handleKeepMeLogged = (event) => {
      setKeepMeLogged(!keepMeLogged);
    };
    
  useEffect (( ) => {
    if(keepMeLogged) {
      localStorage.setItem('id', loggedUser.id.toString());
      localStorage.setItem('token', loggedUser.token.toString());
    } else {
      sessionStorage.setItem('id', loggedUser.id.toString());
      sessionStorage.setItem('token', loggedUser.token.toString());
    }
  }, []); 
    

  const handleSubmit = () => {
      fetch(`http://localhost:3010/api/SignIn`, {
      method: 'POST',
      headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json'
      },
      body: JSON.stringify(user)
     }).then(res => res.json())
      .then(data => {
        setLoggedUser(data.result)
        setRedirect(true)
      
    })
    .catch(error => console.log(error))
    
};  

console.log(user);
console.log(loggedUser);

      return (
        <Container className= 'formulario'>
            <h1>Sign in to your account</h1>
            <Form onSubmit= {handleSubmit}>
            <Form.Field> 
                <label htmlFor="username">Username: </label>
                <input
                type="text"
                value={user.EmailAddress}
                placeholder="Enter your email address here"
                id="EmailAddress"
                onChange={handleChange}
              />
            </Form.Field>  

            <Form.Field> 
                <label htmlFor="password">Password: </label>
                <input
                type="password"
                value={user.Password}
                placeholder="Enter your password here"
                id="Password"
                onChange={handleChange}
                />
            </Form.Field> 
            <Form.Field>
              <Checkbox label='Remember me!   ' onClick ={handleRememberMe} />
              <Checkbox label='Keep me logged in!' onClick ={handleKeepMeLogged} />
            </Form.Field>
              <Button type="submit" id="submit_btn" >Sign in</Button>
            </Form>
        
        { redirect &&
            <Redirect to='/user/homepage' />
        }

        </Container>


    );



};