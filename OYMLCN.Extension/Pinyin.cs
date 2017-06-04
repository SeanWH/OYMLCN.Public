using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace OYMLCN
{
    /// <summary>
    /// PinYinExtension
    /// </summary>
    public static class PinYinExtension
    {
        /// <summary>
        /// 拼音Model
        /// </summary>
        public class PinyinModel
        {
            /// <summary>
            /// 拼音Model
            /// </summary>
            public PinyinModel()
            {
                TotalPinYin = new List<string>();
                FirstPinYin = new List<string>();
            }
            /// <summary>
            /// 全拼列表
            /// </summary>
            public List<string> TotalPinYin { get; set; }
            /// <summary>
            /// 首字母列表
            /// </summary>
            public List<string> FirstPinYin { get; set; }
        }

        /// <summary>
        /// 根据汉字获取拼音，如果不是汉字直接返回原字符
        /// </summary>
        /// <param name="str">要转换的汉字</param>
        /// <param name="polyphone">支持多音字</param>
        /// <returns></returns>
        public static PinyinModel Pinyin(this string str, bool polyphone = true)
        {
            var result = new PinyinModel();
            List<string> total = new List<string>(), first = new List<string>();
            string[] strArray = GetStrArray(str);
            foreach (var strChar in strArray)
            {
                string pinyin = strChar, py = strChar;
                if (IsChineseReg(strChar))
                {
                    List<string> a = GetPinyinByOne(strChar, polyphone);
                    if (a.Count() > 0)
                    {
                        pinyin = String.Join(" ", a);
                        py = String.Join(" ", a.Select(d => d.Substring(0, 1)));
                    }
                }
                total.Add(pinyin);
                first.Add(py);
            }
            result.TotalPinYin = handlePolyphone(total);
            result.FirstPinYin = handlePolyphone(first);
            return result;
        }



        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="str">待分割字符串</param>
        /// <returns></returns>
        private static string[] GetStrArray(string str)
        {
            //eachchar每个元素就是一个字。
            string[] eachchar = str.Select(x => x.ToString()).ToArray();
            return eachchar;
        }
        /// <summary>
        /// 判断字符是不是汉字
        /// </summary>
        /// <param name="text">待判断字符或字符串</param>
        /// <returns>true是 false不是</returns>
        public static bool IsChineseReg(this string text) => Regex.IsMatch(text, @"[\u4e00-\u9fbb]+$");
        /// <summary>
        /// 处理多音字，将类似['chang zhang', 'cheng'] 转换成 ['changcheng', 'zhangcheng']
        /// </summary>
        /// <param name="list">转换前列表</param>
        /// <returns>转换后数组</returns>
        private static List<string> handlePolyphone(List<string> list)
        {
            List<string> result = new List<string>(), temp;
            for (var i = 0; i < list.Count(); i++)
            {
                temp = new List<string>();
                var t = list[i].Split(new char[] { ' ' });
                for (var j = 0; j < t.Count(); j++)
                {
                    if (result.Count() > 0)
                        for (var k = 0; k < result.Count(); k++)
                        {
                            string newpy = result[k] + t[j];
                            temp.Add(newpy);
                        }
                    else
                    {
                        string newpy = t[j];
                        temp.Add(newpy);
                    }
                }
                result = temp;
            }
            return result;
        }
        /// <summary>
        /// 根据单个汉字获取拼音
        /// </summary>
        /// <param name="str">单个汉字</param>
        /// <param name="polyphone">是否支持多音字 (否则根据字库中返回第一个拼音)</param>
        /// <returns></returns>
        private static List<string> GetPinyinByOne(string str, bool polyphone)
        {
            var result = Dic.Where(d => d.Chares.Contains(str)).Select(d => d.Pinyin);
            if (polyphone)
                return result.ToList();
            else
                return result.Take(1).ToList();
        }



        private class PinyinDic
        {
            public PinyinDic(string pinyin, string chares)
            {
                this.Pinyin = pinyin;
                this.Chares = chares;
            }
            /// <summary>
            /// 拼音
            /// </summary>
            public string Pinyin;
            /// <summary>
            /// 字表
            /// </summary>
            public string Chares;
        }
        private static List<PinyinDic> _dic;
        /// <summary>
        /// 字典
        /// 收录常用汉字6763个，不支持声调，支持多音字，并按照汉字使用频率由低到高排序
        /// </summary>
        private static List<PinyinDic> Dic
        {
            get
            {
                if (_dic != null)
                    return _dic;
                _dic = new List<PinyinDic>();
                _dic.Add(new PinyinDic("a", "阿啊呵腌嗄吖锕"));
                _dic.Add(new PinyinDic("e", "额阿俄恶鹅遏鄂厄饿峨扼娥鳄哦蛾噩愕讹锷垩婀鹗萼谔莪腭锇颚呃阏屙苊轭"));
                _dic.Add(new PinyinDic("ai", "爱埃艾碍癌哀挨矮隘蔼唉皑哎霭捱暧嫒嗳瑷嗌锿砹"));
                _dic.Add(new PinyinDic("ei", "诶"));
                _dic.Add(new PinyinDic("xi", "系西席息希习吸喜细析戏洗悉锡溪惜稀袭夕洒晰昔牺腊烯熙媳栖膝隙犀蹊硒兮熄曦禧嬉玺奚汐徙羲铣淅嘻歙熹矽蟋郗唏皙隰樨浠忾蜥檄郄翕阋鳃舾屣葸螅咭粞觋欷僖醯鼷裼穸饩舄禊诶菥蓰晞"));
                _dic.Add(new PinyinDic("yi", "一以已意议义益亿易医艺食依移衣异伊仪宜射遗疑毅谊亦疫役忆抑尾乙译翼蛇溢椅沂泄逸蚁夷邑怡绎彝裔姨熠贻矣屹颐倚诣胰奕翌疙弈轶蛾驿壹猗臆弋铱旖漪迤佚翊诒怿痍懿饴峄揖眙镒仡黟肄咿翳挹缢呓刈咦嶷羿钇殪荑薏蜴镱噫癔苡悒嗌瘗衤佾埸圯舣酏劓燚晹祎係"));
                _dic.Add(new PinyinDic("an", "安案按岸暗鞍氨俺胺铵谙庵黯鹌桉埯犴揞厂广"));
                _dic.Add(new PinyinDic("han", "厂汉韩含旱寒汗涵函喊憾罕焊翰邯撼瀚憨捍酣悍鼾邗颔蚶晗菡旰顸犴焓撖琀浛"));
                _dic.Add(new PinyinDic("ang", "昂仰盎肮"));
                _dic.Add(new PinyinDic("ao", "奥澳傲熬凹鳌敖遨鏖袄坳翱嗷拗懊岙螯骜獒鏊艹媪廒聱奡"));
                _dic.Add(new PinyinDic("wa", "瓦挖娃洼袜蛙凹哇佤娲呙腽"));
                _dic.Add(new PinyinDic("yu", "于与育余预域予遇奥语誉玉鱼雨渔裕愈娱欲吁舆宇羽逾豫郁寓吾狱喻御浴愉禹俞邪榆愚渝尉淤虞屿峪粥驭瑜禺毓钰隅芋熨瘀迂煜昱汩於臾盂聿竽萸妪腴圄谕觎揄龉谀俣馀庾妤瘐鬻欤鹬阈嵛雩鹆圉蜮伛纡窬窳饫蓣狳肀舁蝓燠"));
                _dic.Add(new PinyinDic("niu", "牛纽扭钮拗妞忸狃"));
                _dic.Add(new PinyinDic("o", "哦噢喔嚄"));
                _dic.Add(new PinyinDic("ba", "把八巴拔伯吧坝爸霸罢芭跋扒叭靶疤笆耙鲅粑岜灞钯捌菝魃茇"));
                _dic.Add(new PinyinDic("pa", "怕帕爬扒趴琶啪葩耙杷钯筢掱"));
                _dic.Add(new PinyinDic("pi", "被批副否皮坏辟啤匹披疲罢僻毗坯脾譬劈媲屁琵邳裨痞癖陂丕枇噼霹吡纰砒铍淠郫埤濞睥芘蚍圮鼙罴蜱疋貔仳庀擗甓陴玭潎"));
                _dic.Add(new PinyinDic("bi", "比必币笔毕秘避闭佛辟壁弊彼逼碧鼻臂蔽拂泌璧庇痹毙弼匕鄙陛裨贲敝蓖吡篦纰俾铋毖筚荸薜婢哔跸濞秕荜愎睥妣芘箅髀畀滗狴萆嬖襞舭赑"));
                _dic.Add(new PinyinDic("bai", "百白败摆伯拜柏佰掰呗擘捭稗"));
                _dic.Add(new PinyinDic("bo", "波博播勃拨薄佛伯玻搏柏泊舶剥渤卜驳簿脖膊簸菠礴箔铂亳钵帛擘饽跛钹趵檗啵鹁擗踣"));
                _dic.Add(new PinyinDic("bei", "北被备倍背杯勃贝辈悲碑臂卑悖惫蓓陂钡狈呗焙碚褙庳鞴孛鹎邶鐾"));
                _dic.Add(new PinyinDic("ban", "办版半班般板颁伴搬斑扮拌扳瓣坂阪绊钣瘢舨癍"));
                _dic.Add(new PinyinDic("pan", "判盘番潘攀盼拚畔胖叛拌蹒磐爿蟠泮袢襻丬"));
                _dic.Add(new PinyinDic("bin", "份宾频滨斌彬濒殡缤鬓槟摈膑玢镔豳髌傧"));
                _dic.Add(new PinyinDic("bang", "帮邦彭旁榜棒膀镑绑傍磅蚌谤梆浜蒡"));
                _dic.Add(new PinyinDic("pang", "旁庞乓磅螃彷滂逄耪"));
                _dic.Add(new PinyinDic("beng", "泵崩蚌蹦迸绷甭嘣甏堋"));
                _dic.Add(new PinyinDic("bao", "报保包宝暴胞薄爆炮饱抱堡剥鲍曝葆瀑豹刨褒雹孢苞煲褓趵鸨龅勹"));
                _dic.Add(new PinyinDic("bu", "不部步布补捕堡埔卜埠簿哺怖钚卟瓿逋晡醭钸"));
                _dic.Add(new PinyinDic("pu", "普暴铺浦朴堡葡谱埔扑仆蒲曝瀑溥莆圃璞濮菩蹼匍噗氆攵镨攴镤"));
                _dic.Add(new PinyinDic("mian", "面棉免绵缅勉眠冕娩腼渑湎沔黾宀眄"));
                _dic.Add(new PinyinDic("po", "破繁坡迫颇朴泊婆泼魄粕鄱珀陂叵笸泺皤钋钷"));
                _dic.Add(new PinyinDic("fan", "反范犯繁饭泛翻凡返番贩烦拚帆樊藩矾梵蕃钒幡畈蘩蹯燔"));
                _dic.Add(new PinyinDic("fu", "府服副负富复福夫妇幅付扶父符附腐赴佛浮覆辅傅伏抚赋辐腹弗肤阜袱缚甫氟斧孚敷俯拂俘咐腑孵芙涪釜脯茯馥宓绂讣呋罘麸蝠匐芾蜉跗凫滏蝮驸绋蚨砩桴赙菔呒趺苻拊阝鲋怫稃郛莩幞祓艴黻黼鳆"));
                _dic.Add(new PinyinDic("ben", "本体奔苯笨夯贲锛畚坌犇"));
                _dic.Add(new PinyinDic("feng", "风丰封峰奉凤锋冯逢缝蜂枫疯讽烽俸沣酆砜葑唪"));
                _dic.Add(new PinyinDic("bian", "变便边编遍辩鞭辨贬匾扁卞汴辫砭苄蝙鳊弁窆笾煸褊碥忭缏"));
                _dic.Add(new PinyinDic("pian", "便片篇偏骗翩扁骈胼蹁谝犏缏"));
                _dic.Add(new PinyinDic("zhen", "镇真针圳振震珍阵诊填侦臻贞枕桢赈祯帧甄斟缜箴疹砧榛鸩轸稹溱蓁胗椹朕畛浈"));
                _dic.Add(new PinyinDic("biao", "表标彪镖裱飚膘飙镳婊骠飑杓髟鳔灬瘭猋骉"));
                _dic.Add(new PinyinDic("piao", "票朴漂飘嫖瓢剽缥殍瞟骠嘌莩螵"));
                _dic.Add(new PinyinDic("huo", "和活或货获火伙惑霍祸豁嚯藿锪蠖钬耠镬夥灬劐攉嚄喐嚿"));
                _dic.Add(new PinyinDic("bie", "别鳖憋瘪蹩"));
                _dic.Add(new PinyinDic("min", "民敏闽闵皿泯岷悯珉抿黾缗玟愍苠鳘"));
                _dic.Add(new PinyinDic("fen", "分份纷奋粉氛芬愤粪坟汾焚酚吩忿棼玢鼢瀵偾鲼瞓"));
                _dic.Add(new PinyinDic("bing", "并病兵冰屏饼炳秉丙摒柄槟禀枋邴冫靐抦"));
                _dic.Add(new PinyinDic("geng", "更耕颈庚耿梗埂羹哽赓绠鲠"));
                _dic.Add(new PinyinDic("fang", "方放房防访纺芳仿坊妨肪邡舫彷枋鲂匚钫昉"));
                _dic.Add(new PinyinDic("xian", "现先县见线限显险献鲜洗宪纤陷闲贤仙衔掀咸嫌掺羡弦腺痫娴舷馅酰铣冼涎暹籼锨苋蚬跹岘藓燹鹇氙莶霰跣猃彡祆筅顕鱻咁"));
                _dic.Add(new PinyinDic("fou", "不否缶"));
                _dic.Add(new PinyinDic("ca", "拆擦嚓礤"));
                _dic.Add(new PinyinDic("cha", "查察差茶插叉刹茬楂岔诧碴嚓喳姹杈汊衩搽槎镲苴檫馇锸猹"));
                _dic.Add(new PinyinDic("cai", "才采财材菜彩裁蔡猜踩睬啋"));
                _dic.Add(new PinyinDic("can", "参残餐灿惨蚕掺璨惭粲孱骖黪"));
                _dic.Add(new PinyinDic("shen", "信深参身神什审申甚沈伸慎渗肾绅莘呻婶娠砷蜃哂椹葚吲糁渖诜谂矧胂珅屾燊"));
                _dic.Add(new PinyinDic("cen", "参岑涔"));
                _dic.Add(new PinyinDic("san", "三参散伞叁糁馓毵"));
                _dic.Add(new PinyinDic("cang", "藏仓苍沧舱臧伧"));
                _dic.Add(new PinyinDic("zang", "藏脏葬赃臧奘驵"));
                _dic.Add(new PinyinDic("chen", "称陈沈沉晨琛臣尘辰衬趁忱郴宸谌碜嗔抻榇伧谶龀肜棽"));
                _dic.Add(new PinyinDic("cao", "草操曹槽糙嘈漕螬艚屮"));
                _dic.Add(new PinyinDic("ce", "策测册侧厕栅恻"));
                _dic.Add(new PinyinDic("ze", "责则泽择侧咋啧仄箦赜笮舴昃迮帻"));
                _dic.Add(new PinyinDic("zhai", "债择齐宅寨侧摘窄斋祭翟砦瘵哜"));
                _dic.Add(new PinyinDic("dao", "到道导岛倒刀盗稻蹈悼捣叨祷焘氘纛刂帱忉"));
                _dic.Add(new PinyinDic("ceng", "层曾蹭噌"));
                _dic.Add(new PinyinDic("zha", "查扎炸诈闸渣咋乍榨楂札栅眨咤柞喳喋铡蚱吒怍砟揸痄哳齄"));
                _dic.Add(new PinyinDic("chai", "差拆柴钗豺侪虿瘥"));
                _dic.Add(new PinyinDic("ci", "次此差词辞刺瓷磁兹慈茨赐祠伺雌疵鹚糍呲粢"));
                _dic.Add(new PinyinDic("zi", "资自子字齐咨滋仔姿紫兹孜淄籽梓鲻渍姊吱秭恣甾孳訾滓锱辎趑龇赀眦缁呲笫谘嵫髭茈粢觜耔"));
                _dic.Add(new PinyinDic("cuo", "措错磋挫搓撮蹉锉厝嵯痤矬瘥脞鹾"));
                _dic.Add(new PinyinDic("chan", "产单阐崭缠掺禅颤铲蝉搀潺蟾馋忏婵孱觇廛谄谗澶骣羼躔蒇冁"));
                _dic.Add(new PinyinDic("shan", "山单善陕闪衫擅汕扇掺珊禅删膳缮赡鄯栅煽姗跚鳝嬗潸讪舢苫疝掸膻钐剡蟮芟埏彡骟羴"));
                _dic.Add(new PinyinDic("zhan", "展战占站崭粘湛沾瞻颤詹斩盏辗绽毡栈蘸旃谵搌"));
                _dic.Add(new PinyinDic("xin", "新心信辛欣薪馨鑫芯锌忻莘昕衅歆囟忄镡馫"));
                _dic.Add(new PinyinDic("lian", "联连练廉炼脸莲恋链帘怜涟敛琏镰濂楝鲢殓潋裢裣臁奁莶蠊蔹"));
                _dic.Add(new PinyinDic("chang", "场长厂常偿昌唱畅倡尝肠敞倘猖娼淌裳徜昶怅嫦菖鲳阊伥苌氅惝鬯玚"));
                _dic.Add(new PinyinDic("zhang", "长张章障涨掌帐胀彰丈仗漳樟账杖璋嶂仉瘴蟑獐幛鄣嫜"));
                _dic.Add(new PinyinDic("chao", "超朝潮炒钞抄巢吵剿绰嘲晁焯耖怊"));
                _dic.Add(new PinyinDic("zhao", "着照招找召朝赵兆昭肇罩钊沼嘲爪诏濯啁棹笊"));
                _dic.Add(new PinyinDic("zhou", "调州周洲舟骤轴昼宙粥皱肘咒帚胄绉纣妯啁诌繇碡籀酎荮"));
                _dic.Add(new PinyinDic("che", "车彻撤尺扯澈掣坼砗屮唓"));
                _dic.Add(new PinyinDic("ju", "车局据具举且居剧巨聚渠距句拒俱柜菊拘炬桔惧矩鞠驹锯踞咀瞿枸掬沮莒橘飓疽钜趄踽遽琚龃椐苣裾榘狙倨榉苴讵雎锔窭鞫犋屦醵"));
                _dic.Add(new PinyinDic("cheng", "成程城承称盛抢乘诚呈净惩撑澄秤橙骋逞瞠丞晟铛埕塍蛏柽铖酲裎枨琤"));
                _dic.Add(new PinyinDic("rong", "容荣融绒溶蓉熔戎榕茸冗嵘肜狨蝾瑢"));
                _dic.Add(new PinyinDic("sheng", "生声升胜盛乘圣剩牲甸省绳笙甥嵊晟渑眚焺珄昇湦陹竔琞"));
                _dic.Add(new PinyinDic("deng", "等登邓灯澄凳瞪蹬噔磴嶝镫簦戥"));
                _dic.Add(new PinyinDic("zhi", "制之治质职只志至指织支值知识直致执置止植纸拓智殖秩旨址滞氏枝芝脂帜汁肢挚稚酯掷峙炙栉侄芷窒咫吱趾痔蜘郅桎雉祉郦陟痣蛭帙枳踯徵胝栀贽祗豸鸷摭轵卮轾彘觯絷跖埴夂黹忮骘膣踬臸"));
                _dic.Add(new PinyinDic("zheng", "政正证争整征郑丁症挣蒸睁铮筝拯峥怔诤狰徵钲掟"));
                _dic.Add(new PinyinDic("tang", "堂唐糖汤塘躺趟倘棠烫淌膛搪镗傥螳溏帑羰樘醣螗耥铴瑭"));
                _dic.Add(new PinyinDic("chi", "持吃池迟赤驰尺斥齿翅匙痴耻炽侈弛叱啻坻眙嗤墀哧茌豉敕笞饬踟蚩柢媸魑篪褫彳鸱螭瘛眵傺"));
                _dic.Add(new PinyinDic("shi", "是时实事市十使世施式势视识师史示石食始士失适试什泽室似诗饰殖释驶氏硕逝湿蚀狮誓拾尸匙仕柿矢峙侍噬嗜栅拭嘘屎恃轼虱耆舐莳铈谥炻豕鲥饣螫酾筮埘弑礻蓍鲺贳湜"));
                _dic.Add(new PinyinDic("qi", "企其起期气七器汽奇齐启旗棋妻弃揭枝歧欺骑契迄亟漆戚岂稽岐琦栖缉琪泣乞砌祁崎绮祺祈凄淇杞脐麒圻憩芪伎俟畦耆葺沏萋骐鳍綦讫蕲屺颀亓碛柒啐汔綮萁嘁蛴槭欹芑桤丌蜞"));
                _dic.Add(new PinyinDic("chuai", "揣踹啜搋膪"));
                _dic.Add(new PinyinDic("tuo", "托脱拓拖妥驼陀沱鸵驮唾椭坨佗砣跎庹柁橐乇铊沲酡鼍箨柝"));
                _dic.Add(new PinyinDic("duo", "多度夺朵躲铎隋咄堕舵垛惰哆踱跺掇剁柁缍沲裰哚隳"));
                _dic.Add(new PinyinDic("xue", "学血雪削薛穴靴谑噱鳕踅泶彐"));
                _dic.Add(new PinyinDic("chong", "重种充冲涌崇虫宠忡憧舂茺铳艟蟲翀"));
                _dic.Add(new PinyinDic("chou", "筹抽绸酬愁丑臭仇畴稠瞅踌惆俦瘳雠帱"));
                _dic.Add(new PinyinDic("qiu", "求球秋丘邱仇酋裘龟囚遒鳅虬蚯泅楸湫犰逑巯艽俅蝤赇鼽糗"));
                _dic.Add(new PinyinDic("xiu", "修秀休宿袖绣臭朽锈羞嗅岫溴庥馐咻髹鸺貅飍"));
                _dic.Add(new PinyinDic("chu", "出处础初助除储畜触楚厨雏矗橱锄滁躇怵绌搐刍蜍黜杵蹰亍樗憷楮"));
                _dic.Add(new PinyinDic("tuan", "团揣湍疃抟彖"));
                _dic.Add(new PinyinDic("zhui", "追坠缀揣椎锥赘惴隹骓缒"));
                _dic.Add(new PinyinDic("chuan", "传川船穿串喘椽舛钏遄氚巛舡"));
                _dic.Add(new PinyinDic("zhuan", "专转传赚砖撰篆馔啭颛孨"));
                _dic.Add(new PinyinDic("yuan", "元员院原源远愿园援圆缘袁怨渊苑宛冤媛猿垣沅塬垸鸳辕鸢瑗圜爰芫鼋橼螈眢箢掾厵"));
                _dic.Add(new PinyinDic("cuan", "窜攒篡蹿撺爨汆镩"));
                _dic.Add(new PinyinDic("chuang", "创床窗闯幢疮怆"));
                _dic.Add(new PinyinDic("zhuang", "装状庄壮撞妆幢桩奘僮戆壵"));
                _dic.Add(new PinyinDic("chui", "吹垂锤炊椎陲槌捶棰"));
                _dic.Add(new PinyinDic("chun", "春纯醇淳唇椿蠢鹑朐莼肫蝽"));
                _dic.Add(new PinyinDic("zhun", "准屯淳谆肫窀"));
                _dic.Add(new PinyinDic("cu", "促趋趣粗簇醋卒蹴猝蹙蔟殂徂麤"));
                _dic.Add(new PinyinDic("dun", "吨顿盾敦蹲墩囤沌钝炖盹遁趸砘礅"));
                _dic.Add(new PinyinDic("qu", "区去取曲趋渠趣驱屈躯衢娶祛瞿岖龋觑朐蛐癯蛆苣阒诎劬蕖蘧氍黢蠼璩麴鸲磲佢"));
                _dic.Add(new PinyinDic("xu", "需许续须序徐休蓄畜虚吁绪叙旭邪恤墟栩絮圩婿戌胥嘘浒煦酗诩朐盱蓿溆洫顼勖糈砉醑媭珝昫"));
                _dic.Add(new PinyinDic("chuo", "辍绰戳淖啜龊踔辶"));
                _dic.Add(new PinyinDic("zu", "组族足祖租阻卒俎诅镞菹"));
                _dic.Add(new PinyinDic("ji", "济机其技基记计系期际及集级几给积极己纪即继击既激绩急奇吉季齐疾迹鸡剂辑籍寄挤圾冀亟寂暨脊跻肌稽忌饥祭缉棘矶汲畸姬藉瘠骥羁妓讥稷蓟悸嫉岌叽伎鲫诘楫荠戟箕霁嵇觊麂畿玑笈犄芨唧屐髻戢佶偈笄跽蒺乩咭赍嵴虮掎齑殛鲚剞洎丌墼蕺彐芰哜"));
                _dic.Add(new PinyinDic("cong", "从丛匆聪葱囱琮淙枞骢苁璁"));
                _dic.Add(new PinyinDic("zong", "总从综宗纵踪棕粽鬃偬枞腙"));
                _dic.Add(new PinyinDic("cou", "凑辏腠楱"));
                _dic.Add(new PinyinDic("cui", "衰催崔脆翠萃粹摧璀瘁悴淬啐隹毳榱"));
                _dic.Add(new PinyinDic("wei", "为位委未维卫围违威伟危味微唯谓伪慰尾魏韦胃畏帷喂巍萎蔚纬潍尉渭惟薇苇炜圩娓诿玮崴桅偎逶倭猥囗葳隗痿猬涠嵬韪煨艉隹帏闱洧沩隈鲔軎烓"));
                _dic.Add(new PinyinDic("cun", "村存寸忖皴"));
                _dic.Add(new PinyinDic("zuo", "作做座左坐昨佐琢撮祚柞唑嘬酢怍笮阼胙咗"));
                _dic.Add(new PinyinDic("zuan", "钻纂攥缵躜"));
                _dic.Add(new PinyinDic("da", "大达打答搭沓瘩惮嗒哒耷鞑靼褡笪怛妲龘"));
                _dic.Add(new PinyinDic("dai", "大代带待贷毒戴袋歹呆隶逮岱傣棣怠殆黛甙埭诒绐玳呔迨"));
                _dic.Add(new PinyinDic("tai", "大台太态泰抬胎汰钛苔薹肽跆邰鲐酞骀炱"));
                _dic.Add(new PinyinDic("ta", "他它她拓塔踏塌榻沓漯獭嗒挞蹋趿遢铊鳎溻闼譶"));
                _dic.Add(new PinyinDic("dan", "但单石担丹胆旦弹蛋淡诞氮郸耽殚惮儋眈疸澹掸膻啖箪聃萏瘅赕"));
                _dic.Add(new PinyinDic("lu", "路六陆录绿露鲁卢炉鹿禄赂芦庐碌麓颅泸卤潞鹭辘虏璐漉噜戮鲈掳橹轳逯渌蓼撸鸬栌氇胪镥簏舻辂垆"));
                _dic.Add(new PinyinDic("tan", "谈探坦摊弹炭坛滩贪叹谭潭碳毯瘫檀痰袒坍覃忐昙郯澹钽锬"));
                _dic.Add(new PinyinDic("ren", "人任认仁忍韧刃纫饪妊荏稔壬仞轫亻衽"));
                _dic.Add(new PinyinDic("jie", "家结解价界接节她届介阶街借杰洁截姐揭捷劫戒皆竭桔诫楷秸睫藉拮芥诘碣嗟颉蚧孑婕疖桀讦疥偈羯袷哜喈卩鲒骱劼吤"));
                _dic.Add(new PinyinDic("yan", "研严验演言眼烟沿延盐炎燕岩宴艳颜殷彦掩淹阎衍铅雁咽厌焰堰砚唁焉晏檐蜒奄俨腌妍谚兖筵焱偃闫嫣鄢湮赝胭琰滟阉魇酽郾恹崦芫剡鼹菸餍埏谳讠厣罨啱"));
                _dic.Add(new PinyinDic("dang", "当党档荡挡宕砀铛裆凼菪谠氹"));
                _dic.Add(new PinyinDic("tao", "套讨跳陶涛逃桃萄淘掏滔韬叨洮啕绦饕鼗弢"));
                _dic.Add(new PinyinDic("tiao", "条调挑跳迢眺苕窕笤佻啁粜髫铫祧龆蜩鲦"));
                _dic.Add(new PinyinDic("te", "特忑忒铽慝"));
                _dic.Add(new PinyinDic("de", "的地得德底锝"));
                _dic.Add(new PinyinDic("dei", "得"));
                _dic.Add(new PinyinDic("di", "的地第提低底抵弟迪递帝敌堤蒂缔滴涤翟娣笛棣荻谛狄邸嘀砥坻诋嫡镝碲骶氐柢籴羝睇觌"));
                _dic.Add(new PinyinDic("ti", "体提题弟替梯踢惕剔蹄棣啼屉剃涕锑倜悌逖嚏荑醍绨鹈缇裼"));
                _dic.Add(new PinyinDic("tui", "推退弟腿褪颓蜕忒煺"));
                _dic.Add(new PinyinDic("you", "有由又优游油友右邮尤忧幼犹诱悠幽佑釉柚铀鱿囿酉攸黝莠猷蝣疣呦蚴莸莜铕宥繇卣牖鼬尢蚰侑甴"));
                _dic.Add(new PinyinDic("dian", "电点店典奠甸碘淀殿垫颠滇癫巅惦掂癜玷佃踮靛钿簟坫阽"));
                _dic.Add(new PinyinDic("tian", "天田添填甜甸恬腆佃舔钿阗忝殄畋栝掭瑱"));
                _dic.Add(new PinyinDic("zhu", "主术住注助属逐宁著筑驻朱珠祝猪诸柱竹铸株瞩嘱贮煮烛苎褚蛛拄铢洙竺蛀渚伫杼侏澍诛茱箸炷躅翥潴邾槠舳橥丶瘃麈疰"));
                _dic.Add(new PinyinDic("nian", "年念酿辗碾廿捻撵拈蔫鲶埝鲇辇黏惗唸"));
                _dic.Add(new PinyinDic("diao", "调掉雕吊钓刁貂凋碉鲷叼铫铞"));
                _dic.Add(new PinyinDic("yao", "要么约药邀摇耀腰遥姚窑瑶咬尧钥谣肴夭侥吆疟妖幺杳舀窕窈曜鹞爻繇徭轺铫鳐崾珧垚峣"));
                _dic.Add(new PinyinDic("die", "跌叠蝶迭碟爹谍牒耋佚喋堞瓞鲽垤揲蹀"));
                _dic.Add(new PinyinDic("she", "设社摄涉射折舍蛇拾舌奢慑赦赊佘麝歙畲厍猞揲滠"));
                _dic.Add(new PinyinDic("ye", "业也夜叶射野液冶喝页爷耶邪咽椰烨掖拽曳晔谒腋噎揶靥邺铘揲嘢"));
                _dic.Add(new PinyinDic("xie", "些解协写血叶谢械鞋胁斜携懈契卸谐泄蟹邪歇泻屑挟燮榭蝎撷偕亵楔颉缬邂鲑瀣勰榍薤绁渫廨獬躞劦"));
                _dic.Add(new PinyinDic("zhe", "这者着著浙折哲蔗遮辙辄柘锗褶蜇蛰鹧谪赭摺乇磔螫晢喆嚞嗻啫"));
                _dic.Add(new PinyinDic("ding", "定订顶丁鼎盯钉锭叮仃铤町酊啶碇腚疔玎耵掟"));
                _dic.Add(new PinyinDic("diu", "丢铥"));
                _dic.Add(new PinyinDic("ting", "听庭停厅廷挺亭艇婷汀铤烃霆町蜓葶梃莛"));
                _dic.Add(new PinyinDic("dong", "动东董冬洞懂冻栋侗咚峒氡恫胴硐垌鸫岽胨"));
                _dic.Add(new PinyinDic("tong", "同通统童痛铜桶桐筒彤侗佟潼捅酮砼瞳恸峒仝嗵僮垌茼"));
                _dic.Add(new PinyinDic("zhong", "中重种众终钟忠仲衷肿踵冢盅蚣忪锺舯螽夂"));
                _dic.Add(new PinyinDic("dou", "都斗读豆抖兜陡逗窦渎蚪痘蔸钭篼唞"));
                _dic.Add(new PinyinDic("du", "度都独督读毒渡杜堵赌睹肚镀渎笃竺嘟犊妒牍蠹椟黩芏髑"));
                _dic.Add(new PinyinDic("duan", "断段短端锻缎煅椴簖"));
                _dic.Add(new PinyinDic("dui", "对队追敦兑堆碓镦怼憝"));
                _dic.Add(new PinyinDic("rui", "瑞兑锐睿芮蕊蕤蚋枘"));
                _dic.Add(new PinyinDic("yue", "月说约越乐跃兑阅岳粤悦曰钥栎钺樾瀹龠哕刖曱"));
                _dic.Add(new PinyinDic("tun", "吞屯囤褪豚臀饨暾氽"));
                _dic.Add(new PinyinDic("hui", "会回挥汇惠辉恢徽绘毁慧灰贿卉悔秽溃荟晖彗讳诲珲堕诙蕙晦睢麾烩茴喙桧蛔洄浍虺恚蟪咴隳缋哕烜翙"));
                _dic.Add(new PinyinDic("wu", "务物无五武午吴舞伍污乌误亡恶屋晤悟吾雾芜梧勿巫侮坞毋诬呜钨邬捂鹜兀婺妩於戊鹉浯蜈唔骛仵焐芴鋈庑鼯牾怃圬忤痦迕杌寤阢沕"));
                _dic.Add(new PinyinDic("ya", "亚压雅牙押鸭呀轧涯崖邪芽哑讶鸦娅衙丫蚜碣垭伢氩桠琊揠吖睚痖疋迓岈砑"));
                _dic.Add(new PinyinDic("he", "和合河何核盖贺喝赫荷盒鹤吓呵苛禾菏壑褐涸阂阖劾诃颌嗬貉曷翮纥盍翯"));
                _dic.Add(new PinyinDic("wo", "我握窝沃卧挝涡斡渥幄蜗喔倭莴龌肟硪"));
                _dic.Add(new PinyinDic("en", "恩摁蒽奀"));
                _dic.Add(new PinyinDic("n", "嗯唔"));
                _dic.Add(new PinyinDic("er", "而二尔儿耳迩饵洱贰铒珥佴鸸鲕"));
                _dic.Add(new PinyinDic("fa", "发法罚乏伐阀筏砝垡珐"));
                _dic.Add(new PinyinDic("quan", "全权券泉圈拳劝犬铨痊诠荃醛蜷颧绻犭筌鬈悛辁畎"));
                _dic.Add(new PinyinDic("fei", "费非飞肥废菲肺啡沸匪斐蜚妃诽扉翡霏吠绯腓痱芾淝悱狒榧砩鲱篚镄飝"));
                _dic.Add(new PinyinDic("pei", "配培坏赔佩陪沛裴胚妃霈淠旆帔呸醅辔锫"));
                _dic.Add(new PinyinDic("ping", "平评凭瓶冯屏萍苹乒坪枰娉俜鲆"));
                _dic.Add(new PinyinDic("fo", "佛"));
                _dic.Add(new PinyinDic("hu", "和护许户核湖互乎呼胡戏忽虎沪糊壶葫狐蝴弧瑚浒鹄琥扈唬滹惚祜囫斛笏芴醐猢怙唿戽槲觳煳鹕冱瓠虍岵鹱烀轷"));
                _dic.Add(new PinyinDic("ga", "夹咖嘎尬噶旮伽尕钆尜呷"));
                _dic.Add(new PinyinDic("ge", "个合各革格歌哥盖隔割阁戈葛鸽搁胳舸疙铬骼蛤咯圪镉颌仡硌嗝鬲膈纥袼搿塥哿虼吤嗰"));
                _dic.Add(new PinyinDic("ha", "哈蛤铪"));
                _dic.Add(new PinyinDic("xia", "下夏峡厦辖霞夹虾狭吓侠暇遐瞎匣瑕唬呷黠硖罅狎瘕柙"));
                _dic.Add(new PinyinDic("gai", "改该盖概溉钙丐芥赅垓陔戤"));
                _dic.Add(new PinyinDic("hai", "海还害孩亥咳骸骇氦嗨胲醢"));
                _dic.Add(new PinyinDic("gan", "干感赶敢甘肝杆赣乾柑尴竿秆橄矸淦苷擀酐绀泔坩旰疳澉"));
                _dic.Add(new PinyinDic("gang", "港钢刚岗纲冈杠缸扛肛罡戆筻"));
                _dic.Add(new PinyinDic("jiang", "将强江港奖讲降疆蒋姜浆匠酱僵桨绛缰犟豇礓洚茳糨耩"));
                _dic.Add(new PinyinDic("hang", "行航杭巷夯吭桁沆绗颃"));
                _dic.Add(new PinyinDic("gong", "工公共供功红贡攻宫巩龚恭拱躬弓汞蚣珙觥肱廾"));
                _dic.Add(new PinyinDic("hong", "红宏洪轰虹鸿弘哄烘泓訇蕻闳讧荭黉薨轟"));
                _dic.Add(new PinyinDic("guang", "广光逛潢犷胱咣桄"));
                _dic.Add(new PinyinDic("qiong", "穷琼穹邛茕筇跫蛩銎"));
                _dic.Add(new PinyinDic("gao", "高告搞稿膏糕镐皋羔锆杲郜睾诰藁篙缟槁槔"));
                _dic.Add(new PinyinDic("hao", "好号毫豪耗浩郝皓昊皋蒿壕灏嚎濠蚝貉颢嗥薅嚆"));
                _dic.Add(new PinyinDic("li", "理力利立里李历例离励礼丽黎璃厉厘粒莉梨隶栗荔沥犁漓哩狸藜罹篱鲤砺吏澧俐骊溧砾莅锂笠蠡蛎痢雳俪傈醴栎郦俚枥喱逦娌鹂戾砬唳坜疠蜊黧猁鬲粝蓠呖跞疬缡鲡鳢嫠詈悝苈篥轹刕嚟"));
                _dic.Add(new PinyinDic("jia", "家加价假佳架甲嘉贾驾嫁夹稼钾挟拮迦伽颊浃枷戛荚痂颉镓笳珈岬胛袈郏葭袷瘕铗跏蛱恝哿"));
                _dic.Add(new PinyinDic("luo", "落罗络洛逻螺锣骆萝裸漯烙摞骡咯箩珞捋荦硌雒椤镙跞瘰泺脶猡倮蠃囖啰"));
                _dic.Add(new PinyinDic("ke", "可科克客刻课颗渴壳柯棵呵坷恪苛咳磕珂稞瞌溘轲窠嗑疴蝌岢铪颏髁蚵缂氪骒钶锞"));
                _dic.Add(new PinyinDic("qia", "卡恰洽掐髂袷咭葜"));
                _dic.Add(new PinyinDic("gei", "给"));
                _dic.Add(new PinyinDic("gen", "根跟亘艮哏茛"));
                _dic.Add(new PinyinDic("hen", "很狠恨痕哏"));
                _dic.Add(new PinyinDic("gou", "构购够句沟狗钩拘勾苟垢枸篝佝媾诟岣彀缑笱鞲觏遘玽"));
                _dic.Add(new PinyinDic("kou", "口扣寇叩抠佝蔻芤眍筘"));
                _dic.Add(new PinyinDic("gu", "股古顾故固鼓骨估谷贾姑孤雇辜菇沽咕呱锢钴箍汩梏痼崮轱鸪牯蛊诂毂鹘菰罟嘏臌觚瞽蛄酤牿鲴"));
                _dic.Add(new PinyinDic("pai", "牌排派拍迫徘湃俳哌蒎"));
                _dic.Add(new PinyinDic("gua", "括挂瓜刮寡卦呱褂剐胍诖鸹栝呙啩"));
                _dic.Add(new PinyinDic("tou", "投头透偷愉骰亠"));
                _dic.Add(new PinyinDic("guai", "怪拐乖"));
                _dic.Add(new PinyinDic("kuai", "会快块筷脍蒯侩浍郐蒉狯哙"));
                _dic.Add(new PinyinDic("guan", "关管观馆官贯冠惯灌罐莞纶棺斡矜倌鹳鳏盥掼涫"));
                _dic.Add(new PinyinDic("wan", "万完晚湾玩碗顽挽弯蔓丸莞皖宛婉腕蜿惋烷琬畹豌剜纨绾脘菀芄箢"));
                _dic.Add(new PinyinDic("ne", "呢哪呐讷疒"));
                _dic.Add(new PinyinDic("gui", "规贵归轨桂柜圭鬼硅瑰跪龟匮闺诡癸鳜桧皈鲑刽晷傀眭妫炅庋簋刿宄匦攰"));
                _dic.Add(new PinyinDic("jun", "军均俊君峻菌竣钧骏龟浚隽郡筠皲麇捃"));
                _dic.Add(new PinyinDic("jiong", "窘炯迥炅冂扃"));
                _dic.Add(new PinyinDic("jue", "决绝角觉掘崛诀獗抉爵嚼倔厥蕨攫珏矍蹶谲镢鳜噱桷噘撅橛孓觖劂爝"));
                _dic.Add(new PinyinDic("gun", "滚棍辊衮磙鲧绲丨"));
                _dic.Add(new PinyinDic("hun", "婚混魂浑昏棍珲荤馄诨溷阍"));
                _dic.Add(new PinyinDic("guo", "国过果郭锅裹帼涡椁囗蝈虢聒埚掴猓崞蜾呙馘"));
                _dic.Add(new PinyinDic("hei", "黑嘿嗨"));
                _dic.Add(new PinyinDic("kan", "看刊勘堪坎砍侃嵌槛瞰阚龛戡凵莰"));
                _dic.Add(new PinyinDic("heng", "衡横恒亨哼珩桁蘅"));
                _dic.Add(new PinyinDic("mo", "万没么模末冒莫摩墨默磨摸漠脉膜魔沫陌抹寞蘑摹蓦馍茉嘿谟秣蟆貉嫫镆殁耱嬷麽瘼貊貘尛"));
                _dic.Add(new PinyinDic("peng", "鹏朋彭膨蓬碰苹棚捧亨烹篷澎抨硼怦砰嘭蟛堋"));
                _dic.Add(new PinyinDic("hou", "后候厚侯猴喉吼逅篌糇骺後鲎瘊堠"));
                _dic.Add(new PinyinDic("hua", "化华划话花画滑哗豁骅桦猾铧砉舙"));
                _dic.Add(new PinyinDic("huai", "怀坏淮徊槐踝"));
                _dic.Add(new PinyinDic("huan", "还环换欢患缓唤焕幻痪桓寰涣宦垸洹浣豢奂郇圜獾鲩鬟萑逭漶锾缳擐嬛"));
                _dic.Add(new PinyinDic("xun", "讯训迅孙寻询循旬巡汛勋逊熏徇浚殉驯鲟薰荀浔洵峋埙巽郇醺恂荨窨蕈曛獯灥"));
                _dic.Add(new PinyinDic("huang", "黄荒煌皇凰慌晃潢谎惶簧璜恍幌湟蝗磺隍徨遑肓篁鳇蟥癀"));
                _dic.Add(new PinyinDic("nai", "能乃奶耐奈鼐萘氖柰佴艿妳"));
                _dic.Add(new PinyinDic("luan", "乱卵滦峦鸾栾銮挛孪脔娈"));
                _dic.Add(new PinyinDic("qie", "切且契窃茄砌锲怯伽惬妾趄挈郄箧慊"));
                _dic.Add(new PinyinDic("jian", "建间件见坚检健监减简艰践兼鉴键渐柬剑尖肩舰荐箭浅剪俭碱茧奸歼拣捡煎贱溅槛涧堑笺谏饯锏缄睑謇蹇腱菅翦戬毽笕犍硷鞯牮枧湔鲣囝裥踺搛缣鹣蒹谫僭戋趼楗"));
                _dic.Add(new PinyinDic("nan", "南难男楠喃囡赧腩囝蝻婻暔"));
                _dic.Add(new PinyinDic("qian", "前千钱签潜迁欠纤牵浅遣谦乾铅歉黔谴嵌倩钳茜虔堑钎骞阡掮钤扦芊犍荨仟芡悭缱佥愆褰凵肷岍搴箝慊椠汧"));
                _dic.Add(new PinyinDic("qiang", "强抢疆墙枪腔锵呛羌蔷襁羟跄樯戕嫱戗炝镪锖蜣"));
                _dic.Add(new PinyinDic("xiang", "向项相想乡象响香降像享箱羊祥湘详橡巷翔襄厢镶飨饷缃骧芗庠鲞葙蟓"));
                _dic.Add(new PinyinDic("jiao", "教交较校角觉叫脚缴胶轿郊焦骄浇椒礁佼蕉娇矫搅绞酵剿嚼饺窖跤蛟侥狡姣皎茭峤铰醮鲛湫徼鹪僬噍艽挢敫"));
                _dic.Add(new PinyinDic("zhuo", "着著缴桌卓捉琢灼浊酌拙茁涿镯淖啄濯焯倬擢斫棹诼浞禚"));
                _dic.Add(new PinyinDic("qiao", "桥乔侨巧悄敲俏壳雀瞧翘窍峭锹撬荞跷樵憔鞘橇峤诮谯愀鞒硗劁缲"));
                _dic.Add(new PinyinDic("xiao", "小效销消校晓笑肖削孝萧俏潇硝宵啸嚣霄淆哮筱逍姣箫骁枭哓绡蛸崤枵魈婋虓皛啋"));
                _dic.Add(new PinyinDic("si", "司四思斯食私死似丝饲寺肆撕泗伺嗣祀厮驷嘶锶俟巳蛳咝耜笥纟糸鸶缌澌姒汜厶兕"));
                _dic.Add(new PinyinDic("kai", "开凯慨岂楷恺揩锴铠忾垲剀锎蒈嘅"));
                _dic.Add(new PinyinDic("jin", "进金今近仅紧尽津斤禁锦劲晋谨筋巾浸襟靳瑾烬缙钅矜觐堇馑荩噤廑妗槿赆衿卺"));
                _dic.Add(new PinyinDic("qin", "亲勤侵秦钦琴禽芹沁寝擒覃噙矜嗪揿溱芩衾廑锓吣檎螓骎"));
                _dic.Add(new PinyinDic("jing", "经京精境竞景警竟井惊径静劲敬净镜睛晶颈荆兢靖泾憬鲸茎腈菁胫阱旌粳靓痉箐儆迳婧肼刭弪獍璟誩競"));
                _dic.Add(new PinyinDic("ying", "应营影英景迎映硬盈赢颖婴鹰荧莹樱瑛蝇萦莺颍膺缨瀛楹罂荥萤鹦滢蓥郢茔嘤璎嬴瘿媵撄潆煐媖暎锳"));
                _dic.Add(new PinyinDic("jiu", "就究九酒久救旧纠舅灸疚揪咎韭玖臼柩赳鸠鹫厩啾阄桕僦鬏"));
                _dic.Add(new PinyinDic("zui", "最罪嘴醉咀蕞觜"));
                _dic.Add(new PinyinDic("juan", "卷捐圈眷娟倦绢隽镌涓鹃鄄蠲狷锩桊"));
                _dic.Add(new PinyinDic("suan", "算酸蒜狻"));
                _dic.Add(new PinyinDic("yun", "员运云允孕蕴韵酝耘晕匀芸陨纭郧筠恽韫郓氲殒愠昀菀狁沄"));
                _dic.Add(new PinyinDic("qun", "群裙逡麇"));
                _dic.Add(new PinyinDic("ka", "卡喀咖咔咯佧胩"));
                _dic.Add(new PinyinDic("kang", "康抗扛慷炕亢糠伉钪闶"));
                _dic.Add(new PinyinDic("keng", "坑铿吭"));
                _dic.Add(new PinyinDic("kao", "考靠烤拷铐栲尻犒"));
                _dic.Add(new PinyinDic("ken", "肯垦恳啃龈裉"));
                _dic.Add(new PinyinDic("yin", "因引银印音饮阴隐姻殷淫尹荫吟瘾寅茵圻垠鄞湮蚓氤胤龈窨喑铟洇狺夤廴吲霪茚堙"));
                _dic.Add(new PinyinDic("kong", "空控孔恐倥崆箜"));
                _dic.Add(new PinyinDic("ku", "苦库哭酷裤枯窟挎骷堀绔刳喾"));
                _dic.Add(new PinyinDic("kua", "跨夸垮挎胯侉"));
                _dic.Add(new PinyinDic("kui", "亏奎愧魁馈溃匮葵窥盔逵睽馗聩喟夔篑岿喹揆隗傀暌跬蒉愦悝蝰"));
                _dic.Add(new PinyinDic("kuan", "款宽髋"));
                _dic.Add(new PinyinDic("kuang", "况矿框狂旷眶匡筐邝圹哐贶夼诳诓纩"));
                _dic.Add(new PinyinDic("que", "确却缺雀鹊阙瘸榷炔阕悫慤"));
                _dic.Add(new PinyinDic("kun", "困昆坤捆琨锟鲲醌髡悃阃焜鹍堃"));
                _dic.Add(new PinyinDic("kuo", "扩括阔廓蛞"));
                _dic.Add(new PinyinDic("la", "拉落垃腊啦辣蜡喇剌旯砬邋瘌"));
                _dic.Add(new PinyinDic("lai", "来莱赖睐徕籁涞赉濑癞崃疠铼"));
                _dic.Add(new PinyinDic("lan", "兰览蓝篮栏岚烂滥缆揽澜拦懒榄斓婪阑褴罱啉谰镧漤"));
                _dic.Add(new PinyinDic("lin", "林临邻赁琳磷淋麟霖鳞凛拎遴蔺吝粼嶙躏廪檩啉辚膦瞵懔"));
                _dic.Add(new PinyinDic("lang", "浪朗郎廊狼琅榔螂阆锒莨啷蒗稂"));
                _dic.Add(new PinyinDic("liang", "量两粮良辆亮梁凉谅粱晾靓踉莨椋魉墚"));
                _dic.Add(new PinyinDic("lao", "老劳落络牢捞涝烙姥佬崂唠酪潦痨醪铑铹栳耢"));
                _dic.Add(new PinyinDic("mu", "目模木亩幕母牧莫穆姆墓慕牟牡募睦缪沐暮拇姥钼苜仫毪坶"));
                _dic.Add(new PinyinDic("le", "了乐勒肋叻鳓嘞仂泐"));
                _dic.Add(new PinyinDic("lei", "类累雷勒泪蕾垒磊擂镭肋羸耒儡嫘缧酹嘞诔檑畾"));
                _dic.Add(new PinyinDic("sui", "随岁虽碎尿隧遂髓穗绥隋邃睢祟濉燧谇眭荽"));
                _dic.Add(new PinyinDic("lie", "列烈劣裂猎冽咧趔洌鬣埒捩躐劦"));
                _dic.Add(new PinyinDic("leng", "冷愣棱楞塄"));
                _dic.Add(new PinyinDic("ling", "领令另零灵龄陵岭凌玲铃菱棱伶羚苓聆翎泠瓴囹绫呤棂蛉酃鲮柃"));
                _dic.Add(new PinyinDic("lia", "俩"));
                _dic.Add(new PinyinDic("liao", "了料疗辽廖聊寥缪僚燎缭撂撩嘹潦镣寮蓼獠钌尥鹩"));
                _dic.Add(new PinyinDic("liu", "流刘六留柳瘤硫溜碌浏榴琉馏遛鎏骝绺镏旒熘鹨锍"));
                _dic.Add(new PinyinDic("lun", "论轮伦仑纶沦抡囵"));
                _dic.Add(new PinyinDic("lv", "率律旅绿虑履吕铝屡氯缕滤侣驴榈闾偻褛捋膂稆"));
                _dic.Add(new PinyinDic("lou", "楼露漏陋娄搂篓喽镂偻瘘髅耧蝼嵝蒌"));
                _dic.Add(new PinyinDic("mao", "贸毛矛冒貌茂茅帽猫髦锚懋袤牦卯铆耄峁瑁蟊茆蝥旄泖昴瞀"));
                _dic.Add(new PinyinDic("long", "龙隆弄垄笼拢聋陇胧珑窿茏咙砻垅泷栊癃"));
                _dic.Add(new PinyinDic("nong", "农浓弄脓侬哝"));
                _dic.Add(new PinyinDic("shuang", "双爽霜孀泷"));
                _dic.Add(new PinyinDic("shu", "术书数属树输束述署朱熟殊蔬舒疏鼠淑叔暑枢墅俞曙抒竖蜀薯梳戍恕孰沭赎庶漱塾倏澍纾姝菽黍腧秫毹殳疋摅"));
                _dic.Add(new PinyinDic("shuai", "率衰帅摔甩蟀"));
                _dic.Add(new PinyinDic("lve", "略掠锊"));
                _dic.Add(new PinyinDic("ma", "么马吗摩麻码妈玛嘛骂抹蚂唛蟆犸杩"));
                _dic.Add(new PinyinDic("me", "么麽"));
                _dic.Add(new PinyinDic("mai", "买卖麦迈脉埋霾荬劢"));
                _dic.Add(new PinyinDic("man", "满慢曼漫埋蔓瞒蛮鳗馒幔谩螨熳缦镘颟墁鞔嫚"));
                _dic.Add(new PinyinDic("mi", "米密秘迷弥蜜谜觅靡泌眯麋猕谧咪糜宓汨醚嘧弭脒冖幂祢縻蘼芈糸敉沕"));
                _dic.Add(new PinyinDic("men", "们门闷瞒汶扪焖懑鞔钔"));
                _dic.Add(new PinyinDic("mang", "忙盲茫芒氓莽蟒邙硭漭"));
                _dic.Add(new PinyinDic("meng", "蒙盟梦猛孟萌氓朦锰檬勐懵蟒蜢虻黾蠓艨甍艋瞢礞"));
                _dic.Add(new PinyinDic("miao", "苗秒妙描庙瞄缪渺淼藐缈邈鹋杪眇喵冇"));
                _dic.Add(new PinyinDic("mou", "某谋牟缪眸哞鍪蛑侔厶踎"));
                _dic.Add(new PinyinDic("miu", "缪谬"));
                _dic.Add(new PinyinDic("mei", "美没每煤梅媒枚妹眉魅霉昧媚玫酶镁湄寐莓袂楣糜嵋镅浼猸鹛"));
                _dic.Add(new PinyinDic("wen", "文问闻稳温纹吻蚊雯紊瘟汶韫刎璺玟阌揾"));
                _dic.Add(new PinyinDic("mie", "灭蔑篾乜咩蠛"));
                _dic.Add(new PinyinDic("ming", "明名命鸣铭冥茗溟酩瞑螟暝"));
                _dic.Add(new PinyinDic("na", "内南那纳拿哪娜钠呐捺衲镎肭乸"));
                _dic.Add(new PinyinDic("nei", "内那哪馁"));
                _dic.Add(new PinyinDic("nuo", "难诺挪娜糯懦傩喏搦锘"));
                _dic.Add(new PinyinDic("ruo", "若弱偌箬叒"));
                _dic.Add(new PinyinDic("nang", "囊馕囔曩攮"));
                _dic.Add(new PinyinDic("nao", "脑闹恼挠瑙淖孬垴铙桡呶硇猱蛲嫐"));
                _dic.Add(new PinyinDic("ni", "你尼呢泥疑拟逆倪妮腻匿霓溺旎昵坭铌鲵伲怩睨猊妳"));
                _dic.Add(new PinyinDic("nen", "嫩恁"));
                _dic.Add(new PinyinDic("neng", "能"));
                _dic.Add(new PinyinDic("nin", "您恁"));
                _dic.Add(new PinyinDic("niao", "鸟尿溺袅脲茑嬲"));
                _dic.Add(new PinyinDic("nie", "摄聂捏涅镍孽捻蘖啮蹑嗫臬镊颞乜陧聶"));
                _dic.Add(new PinyinDic("niang", "娘酿"));
                _dic.Add(new PinyinDic("ning", "宁凝拧泞柠咛狞佞聍甯寗"));
                _dic.Add(new PinyinDic("nu", "努怒奴弩驽帑孥胬"));
                _dic.Add(new PinyinDic("nv", "女钕衄恧"));
                _dic.Add(new PinyinDic("ru", "入如女乳儒辱汝茹褥孺濡蠕嚅缛溽铷洳薷襦颥蓐"));
                _dic.Add(new PinyinDic("nuan", "暖"));
                _dic.Add(new PinyinDic("nve", "虐疟"));
                _dic.Add(new PinyinDic("re", "热若惹喏"));
                _dic.Add(new PinyinDic("ou", "区欧偶殴呕禺藕讴鸥瓯沤耦怄"));
                _dic.Add(new PinyinDic("pao", "跑炮泡抛刨袍咆疱庖狍匏脬"));
                _dic.Add(new PinyinDic("pou", "剖掊裒"));
                _dic.Add(new PinyinDic("pen", "喷盆湓"));
                _dic.Add(new PinyinDic("pie", "瞥撇苤氕丿潎"));
                _dic.Add(new PinyinDic("pin", "品贫聘频拼拚颦姘嫔榀牝"));
                _dic.Add(new PinyinDic("se", "色塞瑟涩啬穑铯槭歮"));
                _dic.Add(new PinyinDic("qing", "情青清请亲轻庆倾顷卿晴氢擎氰罄磬蜻箐鲭綮苘黥圊檠謦甠暒凊郬靘"));
                _dic.Add(new PinyinDic("zan", "赞暂攒堑昝簪糌瓒錾趱拶"));
                _dic.Add(new PinyinDic("shao", "少绍召烧稍邵哨韶捎勺梢鞘芍苕劭艄筲杓潲"));
                _dic.Add(new PinyinDic("sao", "扫骚嫂梢缫搔瘙臊埽缲鳋"));
                _dic.Add(new PinyinDic("sha", "沙厦杀纱砂啥莎刹杉傻煞鲨霎嗄痧裟挲铩唼歃"));
                _dic.Add(new PinyinDic("xuan", "县选宣券旋悬轩喧玄绚渲璇炫萱癣漩眩暄煊铉楦泫谖痃碹揎镟儇烜翾昍"));
                _dic.Add(new PinyinDic("ran", "然染燃冉苒髯蚺"));
                _dic.Add(new PinyinDic("rang", "让壤攘嚷瓤穰禳"));
                _dic.Add(new PinyinDic("rao", "绕扰饶娆桡荛"));
                _dic.Add(new PinyinDic("reng", "仍扔"));
                _dic.Add(new PinyinDic("ri", "日"));
                _dic.Add(new PinyinDic("rou", "肉柔揉糅鞣蹂"));
                _dic.Add(new PinyinDic("ruan", "软阮朊"));
                _dic.Add(new PinyinDic("run", "润闰"));
                _dic.Add(new PinyinDic("sa", "萨洒撒飒卅仨脎"));
                _dic.Add(new PinyinDic("suo", "所些索缩锁莎梭琐嗦唆唢娑蓑羧挲桫嗍睃惢"));
                _dic.Add(new PinyinDic("sai", "思赛塞腮噻鳃嘥嗮"));
                _dic.Add(new PinyinDic("shui", "说水税谁睡氵"));
                _dic.Add(new PinyinDic("sang", "桑丧嗓搡颡磉"));
                _dic.Add(new PinyinDic("sen", "森"));
                _dic.Add(new PinyinDic("seng", "僧"));
                _dic.Add(new PinyinDic("shai", "筛晒"));
                _dic.Add(new PinyinDic("shang", "上商尚伤赏汤裳墒晌垧觞殇熵绱"));
                _dic.Add(new PinyinDic("xing", "行省星腥猩惺兴刑型形邢饧醒幸杏性姓陉荇荥擤悻硎"));
                _dic.Add(new PinyinDic("shou", "收手受首售授守寿瘦兽狩绶艏扌"));
                _dic.Add(new PinyinDic("shuo", "说数硕烁朔铄妁槊蒴搠"));
                _dic.Add(new PinyinDic("su", "速素苏诉缩塑肃俗宿粟溯酥夙愫簌稣僳谡涑蔌嗉觫甦"));
                _dic.Add(new PinyinDic("shua", "刷耍唰"));
                _dic.Add(new PinyinDic("shuan", "栓拴涮闩"));
                _dic.Add(new PinyinDic("shun", "顺瞬舜吮"));
                _dic.Add(new PinyinDic("song", "送松宋讼颂耸诵嵩淞怂悚崧凇忪竦菘"));
                _dic.Add(new PinyinDic("sou", "艘搜擞嗽嗖叟馊薮飕嗾溲锼螋瞍"));
                _dic.Add(new PinyinDic("sun", "损孙笋荪榫隼狲飧"));
                _dic.Add(new PinyinDic("teng", "腾疼藤滕誊"));
                _dic.Add(new PinyinDic("tie", "铁贴帖餮萜"));
                _dic.Add(new PinyinDic("tu", "土突图途徒涂吐屠兔秃凸荼钍菟堍酴"));
                _dic.Add(new PinyinDic("wai", "外歪崴"));
                _dic.Add(new PinyinDic("wang", "王望往网忘亡旺汪枉妄惘罔辋魍"));
                _dic.Add(new PinyinDic("weng", "翁嗡瓮蓊蕹"));
                _dic.Add(new PinyinDic("zhua", "抓挝爪"));
                _dic.Add(new PinyinDic("yang", "样养央阳洋扬杨羊详氧仰秧痒漾疡泱殃恙鸯徉佯怏炀烊鞅蛘玚旸飏"));
                _dic.Add(new PinyinDic("xiong", "雄兄熊胸凶匈汹芎"));
                _dic.Add(new PinyinDic("yo", "哟唷"));
                _dic.Add(new PinyinDic("yong", "用永拥勇涌泳庸俑踊佣咏雍甬镛臃邕蛹恿慵壅痈鳙墉饔喁"));
                _dic.Add(new PinyinDic("za", "杂扎咱砸咋匝咂拶雥"));
                _dic.Add(new PinyinDic("zai", "在再灾载栽仔宰哉崽甾"));
                _dic.Add(new PinyinDic("zao", "造早遭枣噪灶燥糟凿躁藻皂澡蚤唣"));
                _dic.Add(new PinyinDic("zei", "贼"));
                _dic.Add(new PinyinDic("zen", "怎谮"));
                _dic.Add(new PinyinDic("zeng", "增曾综赠憎锃甑罾缯"));
                _dic.Add(new PinyinDic("zhei", "这"));
                _dic.Add(new PinyinDic("zou", "走邹奏揍诹驺陬楱鄹鲰"));
                _dic.Add(new PinyinDic("zhuai", "转拽"));
                _dic.Add(new PinyinDic("zun", "尊遵鳟樽撙"));
                _dic.Add(new PinyinDic("dia", "嗲"));
                _dic.Add(new PinyinDic("nou", "耨"));
                return _dic;
            }
        }
    }
}
