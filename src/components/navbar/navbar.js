import React from 'react';
import {Menu} from 'element-react/next';
import {withRouter} from 'react-router-dom';

const Navbar = (props) => {
  const onSelect = (index, indexPath, item) => {
    props.history.push(item.props.to);
  }
  return (
    <Menu theme="dark" className="el-menu-demo" mode="horizontal" onSelect={onSelect}>
      <Menu.Item index="1" to="/">Home</Menu.Item>
      <Menu.Item index="2" to="/products">Products</Menu.Item>
    </Menu>
  );
}

export default withRouter(Navbar);
