import React from 'react';
import {BrowserRouter, Route, Switch, Redirect} from 'react-router-dom';
import './App.css';
import Edit from './containers/edit';
import ProductListing from './containers/productListing';
import Home from './components/home';
import NotFound from './components/notFound';

function App() {
  return (
    <BrowserRouter>
      <Switch>
            <Route path="/products/edit/:id" component={Edit}/>
            <Route path="/products" exact component={ProductListing}/>
            <Redirect from="/home" to="/"/>
            <Route path="/" exact component={Home}/>
            <Route component={NotFound}/>
      </Switch>
    </BrowserRouter>
  );
}

export default App;
