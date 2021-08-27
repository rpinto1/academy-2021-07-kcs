import React, {useState} from 'react';
import { Button, Container, Form, Checkbox} from 'semantic-ui-react';


export default function SignInForm() {

    const [user, setUser] = useState({
      EmailAddress: '',
      Password: ''
    });

    
    const [isRobot, setIsRobot] = useState('true');
    
    const handleChange = event => {
      const { id, value } = event.target
      setUser(prevState => ({
          ...prevState,
          [id]: value
      }));
  };

    const submitUser = () => {
/*         if (isRobot){
            return (
                    <div class="ui negative message">
                    <i class="close icon"></i>
                        <div class="header">
                            WE ARE SORRY!
                        </div>
                        <p>Our website is only available to humans, not robots.</p>
                        </div>

            )
        };
        
 */        axios.post("api/user/authenticate", user)
             .catch ((error) => {console.log(error);});
             console.log(user);

    };
  
    

    
      return (
        <Container className= 'form'>
          <h1>Sign in to your account</h1>
        <Form onSubmit= {console.log(user)}>
        <Form.Field> 
          <label>Username</label>
          <input
            type='email'
              placeholder='Enter your username'
              value={user.EmailAddress}
              id="EmailAddress"
              onChange={handleChange}
          />
          </Form.Field>  

          <Form.Field> 
            <label>Password</label>
            <input
              type='password'
              value={user.Password}
              placeholder='Enter your password'
              id="Password"
              onChange={handleChange}
            />
         </Form.Field> 

         <Form.Field>
            <Checkbox type='checkbox' 
            name='isRobot' 
            label='Are you a robot?' 
            value= {isRobot}
            onChange= {() => setIsRobot('false')}
            required/>        
            </Form.Field>
          
          <Button type="submit">Sign in</Button>
          </Form>
        
        </Container>

      );
    
    

};
