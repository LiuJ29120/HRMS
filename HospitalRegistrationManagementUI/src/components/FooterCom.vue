<template>
  <div id="footer" class="ft">
    <div class="ft-container">
      <div class="ft-nav">
        <a href="/">首页</a>
        <a href="https://www.baidu.com/index.htm" target="_blank">关于我们</a>
        <a href="https://www.baidu.com/index.htm" target="_blank">媒体报道</a>
        <a href="https://www.baidu.com/index.htm" target="_blank">企业团购</a>
        <a href="https://www.baidu.com/index.htm" target="_blank">医院优势</a>
        <a href="https://www.baidu.com/index.htm" target="_blank">友情链接</a>
        <a href="https://www.baidu.com/index.htm" target="_blank">在线补款</a>
        <a href="https://www.baidu.com/index.htm" target="_blank">为什么选择我们</a>
      </div>
      <div class="ft-link">
        <span>友情链接</span>
        <a href="https://www.baidu.com/index.htm" target="_blank">百度</a>
        <a href="https://www.baidu.com/index.htm" target="_blank">百度</a>
        <a href="https://www.baidu.com/index.htm" target="_blank">百度</a>
        <a href="https://www.baidu.com/index.htm" target="_blank">百度</a>
        <a href="https://www.baidu.com/index.htm" target="_blank">百度</a>
        <a href="https://www.baidu.com/index.htm" target="_blank">百度</a>
        <a href="https://www.baidu.com/index.htm" target="_blank">百度</a>
        <a href="https://www.baidu.com/index.htm" target="_blank">百度</a>
        <a href="https://www.baidu.com/index.htm" target="_blank">百度</a>
        <a href="https://www.baidu.com/index.htm" target="_blank">百度</a>
        <a href="https://www.baidu.com/index.htm" target="_blank">百度</a>
        <a href="https://www.baidu.com/index.htm" target="_blank">百度</a>
        <a href="https://www.baidu.com/index.htm" target="_blank">百度</a>
        <a href="https://www.baidu.com/index.htm" target="_blank">百度</a>
      </div>
    </div>
  </div>
</template>

<style lang="scss">
#footer {
  font-size: 0;
  background-color: #020628;

  .ft-container {
    width: 1200px;
    margin: 0 auto;
    padding: 40px 0 46px;
    text-align: center;

    .ft-nav {
      a {
        display: inline-block;
        font-size: 16px;
        line-height: 16px;
        color: #fff;
      }

      a+a {
        padding-left: 28px;
        margin-left: 33px;
        border-left: 1px solid #fff;
      }
    }

    .ft-link {
      margin-top: 20px;

      span {
        font-size: 12px;
        line-height: 16px;
        margin-right: 4px;
        font-weight: bold;
        color: #71797f;
      }

      a {
        font-size: 12px;
        line-height: 16px;
        color: #71797f;
        margin: 0 2px;
      }
    }
  }
}
</style>
